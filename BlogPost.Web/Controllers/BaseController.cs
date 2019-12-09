using System.Linq;
using System.Security.Claims;
using AutoMapper;
using BlogPost.Bll.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected int GetCurrentUserId()
        {
            var strUserId = GetClaim(ClaimTypes.NameIdentifier);
            if (!int.TryParse(strUserId, out var userId))
            {
                throw new UnauthorizedException("User is not signed in");
            }

            return userId;
        }

        protected string GetClaim(string claimType)
        {
            var value = User.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
            return value;
        }
    }
}