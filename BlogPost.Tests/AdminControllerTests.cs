using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Tests.Fakes;
using BlogPost.Web.Controllers;
using BlogPost.Web.Infrastructure;
using BlogPost.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Tests
{
    public class AdminControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserManager> _userManager;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
    
            _userManager = new Mock<IUserManager>();
            _controller = new AdminController(_mapper, _userManager.Object);
        }

        [Fact]
        public async Task ListUsers_ViewName_Ok()
        {
            var result = await _controller.GetUsers();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewName == "ListUsers");
        }

        [Fact]
        public async Task ListUsers_Model_Ok()
        {
            _userManager.Setup(repo => repo.GetUsersAsync()).Returns(GetTestUsers());

            var result = await _controller.GetUsers();

            var viewResult = Assert.IsType<ViewResult>(result);
            var users = Assert.IsAssignableFrom<List<UserViewModel>>(viewResult.Model);
            Assert.True(users.Count == 4);
        }

        [Fact]
        public async Task ListUsers_GetUsers_Exception()
        {
            _userManager.Setup(repo => repo.GetUsersAsync()).Throws(new FakeExcepsion());
            await Assert.ThrowsAsync<FakeExcepsion>(() => _controller.GetUsers());
        }

        private async Task<IList<UserDto>> GetTestUsers()
        {
            var users = new List<UserDto>
            {
                new UserDto { Id=1, UserName="Tom"},
                new UserDto { Id=2, UserName="Alice"},
                new UserDto { Id=3, UserName="Sam"},
                new UserDto { Id=4, UserName="Kate"}
            };

            return await Task.FromResult(users);
        }
    }
}
