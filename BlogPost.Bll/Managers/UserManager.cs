using System;
using System.Linq;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Dal.Identities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BlogPost.Dal.Interfaces.Repositories;
using BlogPost.Dal.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Bll.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IImageRepository _imageRepository;

        public UserManager(
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IImageRepository imageRepository)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _imageRepository = imageRepository;
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

        public async Task<string> GenerateEmailConfirmationTokenAsync(int? userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return code;
        }

        public async Task ConfirmEmailAsync(int? userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new NotImplementedException();
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<UserDto> SignInAsync(string email, bool rememberMe, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("you didnt confirmed your email!");
            }

            var result = await _signInManager
                .PasswordSignInAsync(user, password, rememberMe, false);

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

        public async Task<UserDto> GetUserDetailsAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user == null)
            {
                return null;
            }
            if (user.ProfileImageId.HasValue)
            {
                user.ProfileImage = await _imageRepository.GetAsync(user.ProfileImageId.Value);
            }
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<UserDto> GetUserNameAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<UserDto> GetUserEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _userManager.DeleteAsync(user);
        }

        public async Task<IList<UserDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach(var user in users)
            {
                if (user.ProfileImageId.HasValue)
                {
                    user.ProfileImage = await _imageRepository.GetAsync(user.ProfileImageId.Value);
                }
            }
            var usersDto = _mapper.Map<IList<UserDto>>(users);

            return usersDto;
        }

        public async Task SetProfileImageAsync(int userId, ImageDto imageDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var image = _mapper.Map<ImageEntity>(imageDto);
            user.ProfileImage = image;

            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(int? userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task ResetPasswordAsync(int? userId, string token, string password)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.ResetPasswordAsync  (user, token, password);
        }

        public async Task<UserDto> FindByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<bool> IsEmailInUseAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return false;
            }
            return true; ;
        }

        public async Task<IdentityResult> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result;
        }

        public async Task AddToRolesAsync(int userId, IEnumerable<string> addedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _userManager.AddToRolesAsync(user, addedRoles);
        }

        public async Task RemoveFromRolesAsync(int userId, IEnumerable<string> removedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public async Task<IList<string>> GetRolesAsync(int? userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<IList<string>> GetRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }
    }
}