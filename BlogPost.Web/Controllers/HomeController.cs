using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogPost.Web.Models;
using BlogPost.Bll.Managers.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using BlogPost.Web.Models.Blogs;
using System.Collections.Generic;

namespace BlogPost.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogManager _blogManager;
        private readonly IUserManager _userManager;

        public HomeController(
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
            var blogs = await _blogManager.GetBlogsWithUsers();
            var models = Mapper.Map<IEnumerable<BlogViewModel>>(blogs);

            return View(models);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
