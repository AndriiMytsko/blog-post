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
    public class AccountController : Controller
    {
        private IMapper _mapper;
        private IUserManager _userManager;

        public AccountController(
            IMapper mapper,
            IUserManager userManager)
            :base()
        {
            _mapper = mapper;
            _userManager = userManager;
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
            var user = _mapper.Map<UserDto>(model);
            await _userManager.CreateAsync(user, model.Password);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _userManager
                .SignInAsync(model.Email, model.Password);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync();

            return View();
        }
    }
}
