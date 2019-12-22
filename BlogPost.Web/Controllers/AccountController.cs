using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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

        [HttpPost]
        public IActionResult SetImage(IFormFile uploadedFile)
        {
            byte[] imageData = null;

            if (uploadedFile != null)
            {

                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
            }

            var userId = GetCurrentUserId();
            _userManager.SetProfileImage(userId, imageData);

            return RedirectToAction("Index");
        }
    }
}
