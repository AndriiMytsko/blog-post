using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogPost.Web.Models;
using BlogPost.Bll.Managers.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogPost.Web.Models.Posts;

namespace BlogPost.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPostManager _postManager;

        public HomeController(IMapper mapper,IPostManager postManager)
            : base(mapper)
        {
            _postManager = postManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postManager.GetLastPosts();
            var models = Mapper.Map<IList<PostViewModel>>(posts);

            return View("Index", models);
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
