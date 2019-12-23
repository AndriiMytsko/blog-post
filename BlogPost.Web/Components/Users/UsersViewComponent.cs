using AutoMapper;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPost.Web.Components.Users
{
    public class UsersViewComponent: ViewComponent
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public UsersViewComponent(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var user = await _userManager.GetUserDetails(userId);
            var model = _mapper.Map<UserDetailsViewModel>(user);

            return View(model);
        }
    }
}
