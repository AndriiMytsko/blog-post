using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Posts;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostManager _postManager;

        public PostsController(
            IMapper mapper,
            IPostManager postManager)
        {
            _mapper = mapper;
            _postManager = postManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postManager.GetAllPosts();
            var models = _mapper.Map<IList<PostViewModel>>(posts);

            return View(models);
        }

        public IActionResult CreatePost(int blogId)
        {
            return View(new CreatePostViewModel { BlogId = blogId });
        }

        public async Task<IActionResult> ConfirmCreatePost(CreatePostViewModel post)
        {
            var postDto = _mapper.Map<PostDto>(post);
            await _postManager.CreatePost(postDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PostDetails(PostDetailsViewModel post)
        {
            var postDto = await _postManager.GetPost(post.Id);
            var postModel = _mapper.Map<PostDetailsViewModel>(postDto);
            return View(postModel);
        }

        public async Task<IActionResult> EditPost(UpdatePostViewModel post)
        {
            var postDto = await _postManager.GetPost(post.Id);
            var postModel = _mapper.Map<UpdatePostViewModel>(postDto);

            return View(postModel);
        }

        public async Task<IActionResult> ConfirmEditPost(UpdatePostViewModel post)
        {
            var postDto = _mapper.Map<PostDto>(post);
            await _postManager.UpdatePost(postDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePost(PostViewModel post)
        {
            var postDto = await _postManager.GetPost(post.Id);
            var postModel = _mapper.Map<PostViewModel>(postDto);

            return View(postModel);
        }

        public async Task<IActionResult> ConfirmDeletePost(PostViewModel post)
        {
            await _postManager.DeletePost(post.Id);

            return RedirectToAction("Index");
        }
    }
}