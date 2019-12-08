using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogManager _blogManager;
        private readonly IUserManager _userManager;

        public BlogsController(
            IMapper mapper, 
            IBlogManager blogManager,
            IUserManager userManager)
        : base(mapper)
        {
            _blogManager = blogManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogManager.GetAllBlogs();
            var models = Mapper.Map<IList<BlogViewModel>>(blogs);

            return View(models);
        }

        public IActionResult CreateBlog()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmCreateBlog(CreateBlogViewModel blog)
        {
            var blogDto = Mapper.Map<BlogDto>(blog);

            blogDto.UserId = GetCurrentUserId();
            await _blogManager.CreateBlog(blogDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BlogDetails(BlogViewModel blog)
        {
            var blogDto = await _blogManager.GetBlogWithPosts(blog.Id);
            var blogModel = Mapper.Map<BlogViewModel>(blogDto);

            return View(blogModel);
        }

        public async Task<IActionResult> EditBlog(UpdateBlogViewModel blog)
        {
            var blogDto = await _blogManager.GetBlog(blog.Id);
            var blogModel = Mapper.Map<UpdateBlogViewModel>(blogDto);

            return View(blogModel);
        }

        public async Task<IActionResult> ConfirmEditBlog(UpdateBlogViewModel blog)
        {
            var blogDto = Mapper.Map<BlogDto>(blog);
            await _blogManager.UpdateBlog(blogDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBlog(BlogViewModel blog)
        {
            var blogDto = await _blogManager.GetBlog(blog.Id);
            var blogModel = Mapper.Map<BlogViewModel>(blogDto);

            return View(blogModel);
        }

        public async Task<IActionResult> ConfirmDeleteBlog(BlogViewModel blog)
        {
            await _blogManager.DeleteBlog(blog.Id);

            return RedirectToAction("Index");
        }
    }
}