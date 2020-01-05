using BlogPost.Bll.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<int> CreateAsync(UserDto user, string password);
        Task<UserDto> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<UserDto> GetUserDetails(int userId);
        Task SetProfileImageAsync(int userId, ImageDto imageDto);
    }
}
