using System;
using System.Linq;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Dal.Identities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserManager(
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<int> CreateAsync(UserDto userDto, string password)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return user.Id;
            }

            var msg = result.Errors.Select(x => $"[code: {x.Code}: {x.Description}]");
            throw new Exception($"user cannot be created; details: {msg}");
        }

        public async Task<UserDto> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _signInManager
                .PasswordSignInAsync(user, password, false, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("wrong login or password");
            }

            var entity = _mapper.Map<UserDto>(user);
            return entity;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetUserDetails(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}

