using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> UserDetails()
        {
            var userId = GetCurrentUserId();
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
    }
}
