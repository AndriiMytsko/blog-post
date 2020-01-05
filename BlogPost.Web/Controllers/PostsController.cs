using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostManager _postManager;
        private readonly IUserManager _userManager;

        public PostsController(
            IMapper mapper,
            IPostManager postManager,
            IUserManager userManager)
        : base(mapper)
        {
            _postManager = postManager;
            _userManager = userManager;
        }

        public IActionResult CreatePost(int blogId)
        {
            return View(new CreatePostViewModel { BlogId = blogId });
        }

        public async Task<IActionResult> ConfirmCreatePost(CreatePostViewModel post)
        {
            var postDto = Mapper.Map<PostDto>(post);
            postDto.User = User.CreateUserDto();

            await _postManager.CreatePost(postDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PostDetails(int id)
        {
            var postDto = await _postManager.GetPost(id);
            var postModel = Mapper.Map<PostDetailsViewModel>(postDto);
            return View(postModel);
        }

        public async Task<IActionResult> EditPost(UpdatePostViewModel post)
        {
            var postDto = await _postManager.GetPost(post.Id);
            var postModel = Mapper.Map<UpdatePostViewModel>(postDto);

            return View(postModel);
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
            var postModel = Mapper.Map<PostViewModel>(postDto);

            return View(postModel);
        }

        public async Task<IActionResult> ConfirmDeletePost(PostViewModel post)
        {
            await _postManager.DeletePost(post.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}