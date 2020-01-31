using BlogPost.Bll.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<int> CreateAsync(UserDto user, string password);
        Task<UserDto> SignInAsync(string email, bool rememberMe, string password);
        Task SignOutAsync();
        Task<UserDto> GetUserDetailsAsync(int userId);
        Task<UserDto> GetUserNameAsync(int userId);
        Task<UserDto> GetUserEmailAsync(string email);
        Task ConfirmEmailAsync(int? userId, string code);
        Task DeleteUserAsync(int userId);
        Task<IList<UserDto>> GetUsersAsync();
        Task SetProfileImageAsync(int userId, ImageDto imageDto);
        Task<string> GenerateEmailConfirmationTokenAsync(int? userId);
        Task<string> GeneratePasswordResetTokenAsync(int? userId);
        Task ResetPasswordAsync(int? userId, string code, string password);
        Task<UserDto> FindByIdAsync(int id);
        Task<IdentityResult> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task AddToRolesAsync(int userId, IEnumerable<string> addedRoles);
        Task RemoveFromRolesAsync(int userId, IEnumerable<string> removedRoles);
        Task<IList<string>> GetRolesAsync(int? userId);
        Task<bool> IsEmailInUseAsync(string email);
    }
}
