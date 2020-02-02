using BlogPost.Bll.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace BlogPost.Tests.Fakes
{
    public static class TestExtension
    {
        public static UserDto SetupUser(this ControllerBase controller, int id)
        {
            var user = new UserDto
            {
                Id = id
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, id.ToString())
                    }))
                }
            };

            return user;
        }

        //public static string UrlReturn(this ControllerBase controller)
        //{

        //}
    }
}
