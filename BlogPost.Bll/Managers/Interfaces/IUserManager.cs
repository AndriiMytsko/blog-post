using BlogPost.Bll.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<int> CreateAsync(UserDto user, string password);
        Task<UserDto> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<int> GetUserId(string name);
    }
}
