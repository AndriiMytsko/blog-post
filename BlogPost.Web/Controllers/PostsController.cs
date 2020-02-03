using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    [Authorize]
    public class PostsController : BaseController
    {
        private readonly IPostManager _postManager;

        public PostsController(
            IMapper mapper,
            IPostManager postManager)
        : base(mapper)
        {
            _postManager = postManager;
        }

        public IActionResult CreatePost(int blogId)
        {
            return View(new CreatePostViewModel 
            { 
                BlogId = blogId 
            });
        }

        public async Task<IActionResult> ConfirmCreatePost(CreatePostViewModel post)
        {
            var postDto = Mapper.Map<PostDto>(post);
            postDto.User = User.CreateUserDto();

            await _postManager.CreatePost(postDto);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> PostDetails(int? id)
        {
            var postDto = await _postManager.GetPost(id.Value);
            if (postDto == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<PostDetailsViewModel>(postDto);
            return View("PostDetails", model);
        }

        public async Task<IActionResult> EditPost(UpdatePostViewModel post)
        {
            var postDto = await _postManager.GetPost(post.Id);
            if (postDto == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<UpdatePostViewModel>(postDto);

            return View(model);
        }

        public async Task<IActionResult> ConfirmEditPost(UpdatePostViewModel post)
        {
            var postDto = Mapper.Map<PostDto>(post);
            postDto.User = User.CreateUserDto();
            await _postManager.UpdatePost(postDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeletePost(PostViewModel post)
        {
            var postDto = await _postManager.GetPost(post.Id);
            if(postDto == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<PostViewModel>(postDto);

            return View(model);
        }

        public async Task<IActionResult> ConfirmDeletePost(PostViewModel post)
        {
            await _postManager.DeletePost(post.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}