using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogPost.Web.Models;
using BlogPost.Bll.Managers.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using BlogPost.Web.Models.Blogs;
using System.Collections.Generic;
using BlogPost.Web.Models.Posts;

namespace BlogPost.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogManager _blogManager;
        private readonly IPostManager _postManager;
        private readonly IUserManager _userManager;

        public HomeController(
            IMapper mapper,
            IBlogManager blogManager,
             IPostManager postManager,
            IUserManager userManager)
        : base(mapper)
        {
            _blogManager = blogManager;
            _postManager = postManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postManager.GetPostsWithUsers();
            var models = Mapper.Map<IList<PostDetailsViewModel>>(posts);

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
