using System.Linq;
using System.Security.Claims;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Exceptions;

namespace BlogPost.Web.Infrastructure.Extensions
{
    public static class UserExtentions
    {
        public static UserDto CreateUserDto(this ClaimsPrincipal user)
        {
            return new UserDto { Id = user.GetId() };
        }

        public static int GetId(this ClaimsPrincipal user)
        {
            var strUserId = user.GetClaim(ClaimTypes.NameIdentifier);
            if (!int.TryParse(strUserId, out var userId))
            {
                throw new UnauthorizedException("User is not signed in");
            }

            return userId;
        }

        private static string GetClaim(this ClaimsPrincipal user, string claimType)
        {
            var value = user.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
            return value;
        }
    }
}
