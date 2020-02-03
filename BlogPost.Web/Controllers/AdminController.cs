using AutoMapper;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IUserManager _userManager;

        public AdminController(IMapper mapper, IUserManager userManager)
            : base(mapper)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.GetUsersAsync();
            var models = Mapper.Map<IList<UserViewModel>>(users);

            return View("ListUsers", models);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int? userId)
        {
            if (userId.HasValue){
                await _userManager.DeleteUserAsync(userId.Value);
                return View("ListUsers");
            }
            return RedirectToAction("Error");
        }
    }
}
