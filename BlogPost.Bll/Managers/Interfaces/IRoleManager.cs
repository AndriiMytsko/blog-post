using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPost.Bll.DTOs;
using BlogPost.Dal.Identities;
using Microsoft.AspNetCore.Identity;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IRoleManager
    {
        Task<IdentityResult> CreateAsync(RoleDto roleDto);
        Task<IdentityResult> DeleteAsync(int? id);
        Task<IList<RoleDto>> GetRolesAsync();
    }
}
