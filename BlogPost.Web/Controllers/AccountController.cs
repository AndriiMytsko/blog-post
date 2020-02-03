using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPost.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IEmailManager _emailManager;


        public AccountController(
            IMapper mapper,
            IUserManager userManager,
             IEmailManager emailManager)
            : base(mapper)
        {
            _userManager = userManager;
            _emailManager = emailManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmLogin(LoginViewModel model)
        {
            await _userManager.SignInAsync(model.Email, model.RememberMe, model.Password);

            return Redirect("~/");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var result = await _userManager.IsEmailInUseAsync(email);

            if (!result)
            {
                return Json(result);
            }
            else
            {
                return Json($"Email is already in use!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync();

            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            var userDto = await _userManager.GetUserDetailsAsync(id.Value);
            if (userDto == null)
            {
                return NotFound();
            }

            var user = Mapper.Map<UserDetailsViewModel>(userDto);

            return View("UserDetails", user);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.GetId();

            var userDto = await _userManager.GetUserDetailsAsync(userId);

            var user = Mapper.Map<UserDetailsViewModel>(userDto);

            return View("Profile", user);
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
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserDto>(model);
                var userId = await _userManager.CreateAsync(user, model.Password);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);

                var callbackUrl = Url.EmailConfirmationLink(userId.ToString(), code, HttpContext.Request.Scheme);

                await _emailManager.SendEmailAsync(model.Email, "Confirm your account", callbackUrl);

                return Content("Для завершення реєстрації перевірити електронну пошту та перейдіть за посиланнями, вказаними в письмі");
            }

            return RedirectToAction("Home", "Index", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int? userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            await _userManager.ConfirmEmailAsync(userId, code);

            return Redirect("~/");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {

            var user = await _userManager.GetUserEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            //if (user.Id.HasValue || !(await _userManager.IsEmailConfirmedAsync(user.Id.Value)))
            //{
            //    return View("ForgotPasswordConfirmation");
            //}

            var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = Url.ResetPasswordCallbackLink(user.Id.ToString(), code, HttpContext.Request.Scheme);

            await _emailManager.SendEmailAsync(model.Email, "Reset Password", callbackUrl);

            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.GetUserEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

            return View("ResetPasswordConfirmation");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.FindByIdAsync(User.GetId());
            if (user == null)
            {
                return NotFound();
            }
            var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            IdentityResult result =
            await _userManager.ChangePasswordAsync(User.GetId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", model);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Error");  
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetImage(IFormFile uploadedFile)
        {
            var image = Mapper.Map<ImageDto>(uploadedFile);
            var userId = User.GetId();

            await _userManager.SetProfileImageAsync(userId, image);

            return RedirectToAction("Index", "Home");
        }
    }
}