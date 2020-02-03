using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogManager _blogManager;

        public BlogsController(
            IMapper mapper,
            IBlogManager blogManager)
        : base(mapper)
        {
            _blogManager = blogManager;
        }

        [Authorize]
        public IActionResult CreateBlog()
        {
            return View("CreateBlog");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCreateBlog(CreateBlogViewModel blog)
        {
            var blogDto = Mapper.Map<BlogDto>(blog);

            blogDto.User = User.CreateUserDto();
            await _blogManager.CreateBlog(blogDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> BlogDetails(BlogViewModel blog)
        {
            var blogDto = await _blogManager.GetBlog(blog.Id);
            if (blogDto == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<BlogViewModel>(blogDto);

            return View("BlogDetails", model);
        }

        [Authorize]
        public async Task<IActionResult> EditBlog(UpdateBlogViewModel blog)
        {
            var blogDto = await _blogManager.GetBlog(blog.Id);
            if (blogDto == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<UpdateBlogViewModel>(blogDto);

            return View(model);
        }

        public async Task<IActionResult> ConfirmEditBlog(UpdateBlogViewModel blog)
        {
            var blogDto = Mapper.Map<BlogDto>(blog);
            blogDto.User = User.CreateUserDto();

            await _blogManager.UpdateBlog(blogDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteBlog(BlogViewModel blog)
        {
            var blogDto = await _blogManager.GetBlog(blog.Id);
            if (blogDto == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<BlogViewModel>(blogDto);

            return View(model);
        }

        public async Task<IActionResult> ConfirmDeleteBlog(BlogViewModel blog)
        {
            await _blogManager.DeleteBlog(blog.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}