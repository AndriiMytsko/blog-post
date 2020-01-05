using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPost.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserManager _userManager;

        public AccountController(
            IMapper mapper,
            IUserManager userManager)
            : base(mapper)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmLogin(LoginViewModel model)
        {
            await _userManager.SignInAsync(model.Email, model.Password);

            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync();

            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(int id)
        {
            var userDto = await _userManager.GetUserDetails(id);

            var user = Mapper.Map<UserDetailsViewModel>(userDto);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.GetId();
            var userDto = await _userManager.GetUserDetails(userId);

            var user = Mapper.Map<UserDetailsViewModel>(userDto);

            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = Mapper.Map<UserDto>(model);
            await _userManager.CreateAsync(user, model.Password);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetImage(IFormFile uploadedFile)
        {
            var image = Mapper.Map<ImageDto>(uploadedFile);
            var userId = User.GetId();

            await _userManager.SetProfileImageAsync(userId, image);

            return RedirectToAction("Index", "Home");
        }
    }
}