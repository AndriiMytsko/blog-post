using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Account;
using BlogPost.Web.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Web.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;

        public RolesController(IRoleManager roleManager, IUserManager userManager,
            IMapper mapper)
            : base(mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var rolesDto = await _roleManager.GetRolesAsync();
            var roles = Mapper.Map<IList<RoleViewModel>>(rolesDto);

            return View(roles);
        }

        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            var roleDto = Mapper.Map<RoleDto>(role);

            if (!string.IsNullOrEmpty(role.Name))
            {
                var result = await _roleManager.CreateAsync(roleDto);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int? id)
        {
            if (id.HasValue)
            {
                await _roleManager.DeleteAsync(id.Value);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserList() {
            var usersDto = await _userManager.GetUsersAsync();
            var users = Mapper.Map<IList<UserNameAndEmailViewModel>>(usersDto);

            return View(users);
        }

        public async Task<IActionResult> Edit(string email)
        {
            var user = await _userManager.GetUserEmailAsync(email);

            if (user.Id.HasValue)
            {
                var userRoles = await _userManager.GetRolesAsync(user.Id);
                var allRolesDto = await _roleManager.GetRolesAsync();
                var roles = Mapper.Map<IList<RoleViewModel>>(allRolesDto);

                var model = new ChangeRoleViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserRoles = userRoles,
                    AllRoles = roles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? userId, List<string> roles)
        {
            if (userId.HasValue)
            {
                var userRoles = await _userManager.GetRolesAsync(userId);

                var addedRoles = roles.Except(userRoles);

                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(userId.Value, addedRoles);

                await _userManager.RemoveFromRolesAsync(userId.Value, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
