using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Dal.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Bll.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleManager(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IList<RoleDto>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesDto = _mapper.Map<IList<RoleDto>>(roles);

            return rolesDto;
        }

        public async Task<IdentityResult> CreateAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<ApplicationRole>(roleDto);
            var result = await _roleManager.CreateAsync(role);

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(int? id)
        {
            if (id.HasValue)
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());

                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    return result;
                }
            }

            throw new Exception();
        }
    }
}
