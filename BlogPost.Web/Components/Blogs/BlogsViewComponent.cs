using AutoMapper;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Web.Components.Blogs
{
    public class BlogsViewComponent : ViewComponent
    {
        private readonly IBlogManager _blogManager;
        private readonly IMapper _mapper;

        public BlogsViewComponent(IBlogManager blogManager, IMapper mapper)
        {
            _blogManager = blogManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogManager.GetAllBlogs();
            var models = _mapper.Map<IList<BlogViewModel>>(blogs);

            return View("Blogs", models);
        }
    }
}