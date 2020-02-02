using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Tests.Fakes;
using BlogPost.Web.Controllers;
using BlogPost.Web.Infrastructure;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Tests
{
    public class AccountControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserManager> _userManager;
        private readonly Mock<IEmailManager> _emailManager;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _userManager = new Mock<IUserManager>();
            _emailManager = new Mock<IEmailManager>();
            _controller = new AccountController(_mapper, _userManager.Object, _emailManager.Object);
        }

        [Fact]
        public async Task Login_ViewName_Ok()
        {
            var result = await Task.FromResult(_controller.Login());

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewName == "Login");
        }

        [Fact]
        public async Task ConfirmLogin_CreateUser_Model_ReturnResult_Ok()
        {
            var model = new LoginViewModel
            {
                Email = "Email",
                RememberMe = true,
                Password = "1234"
            };

            var result = await _controller.ConfirmLogin(model);
            var viewResult = Assert.IsType<RedirectResult>(result);

            _userManager.Verify(repo =>
                repo.SignInAsync(
                It.Is<string>(e => e.Equals(model.Email)),
                It.Is<bool>(r => r.Equals(model.RememberMe)),
                It.Is<string>(p => p.Equals(model.Password))
                ));
        }

        [Fact]
        public async Task Loqout_ReturnResult_Ok()
        {
            _userManager.Setup(repo =>
                repo.SignOutAsync());

            var result = await _controller.Logout();


            var viewResult = Assert.IsType<RedirectResult>(result);
            Assert.True(viewResult.Url == "~/");
            _userManager.Verify(x => x.SignOutAsync(), Times.Once);
        }

        [Fact]
        public async Task IsEmailInUse_ReturnResult_Ok()
        {
            var email = "email.gmail.com";
            _userManager.Setup(x => x.IsEmailInUseAsync(email))
                .ReturnsAsync(true);

            var result = await _controller.IsEmailInUse(email);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task IsEmailInUse_EmailIsNull()
        {
            string email = null;

            _userManager.Setup(x => x.IsEmailInUseAsync(email));
            await _controller.IsEmailInUse(email);

            Assert.Null(email);
        }

        [Fact]
        public async Task UserDetails_ReturnResult_Model_ViewName_Ok()
        {
            var userDto =_userManager.Setup(x =>
                x.GetUserDetailsAsync(1))
                .ReturnsAsync(new UserDto { Id = 1});

            var result = await _controller.UserDetails(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserDetailsViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName == "UserDetails");
        }

        [Fact]
        public async Task UserDetails_UserIdIsNull()
        {
            int? id = null;
            await _controller.UserDetails(id);
            Assert.Null(id);
            _userManager.Verify(x => x.GetUserDetailsAsync(1), Times.Never);
        }

        [Fact]
        public async Task UserDetails_UserIsNotFound()
        {
            _userManager.Setup(x => x.GetUserDetailsAsync(1))
                .ReturnsAsync(null as UserDto);

            var result = await _controller.UserDetails(1);

            Assert.IsType<NotFoundResult>(result);
            _userManager.Verify(x => x.GetUserDetailsAsync(1), Times.Once);
        }

        [Fact]
        public async Task Profile_ReturnResult_Model_ViewName()
        {
            _controller.SetupUser(1);

            _userManager.Setup(x => x.GetUserDetailsAsync(1))
                .ReturnsAsync(new UserDto { Id = 1});

            var result = await _controller.Profile();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UserDetailsViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName == "Profile");
        }

        [Fact]
        public async Task Register_ReturnResult_ViewName_Ok()
        {
            var result = await Task.FromResult(_controller.Register());
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.True(viewResult.ViewName == "Register");
        }

        //[Fact]
        //public async Task Register_Ok()
        //{

        //}

        [Fact]
        public async Task ConfirmEmail_ReturnResult_Model_ViewName_Ok()
        {
            var result = await _controller.ConfirmEmail(1, "code");

            _userManager.Verify(x => x.ConfirmEmailAsync(
                It.Is<int>(id => id == 1),
                It.Is<string>(code => code == "code")), Times.Once);

            var viewResult = Assert.IsType<RedirectResult>(result);
            Assert.True(viewResult.Url == "~/");
        }

        [Fact]
        public async Task ConfirmEmail_UserIdINull()
        {
            var result = await _controller.ConfirmEmail(null, "code");

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewName == "Error");

            _userManager.Verify(x => x.ConfirmEmailAsync(null, null), Times.Never);
        }

        [Fact]
        public async Task ForgotPassword_ReturnResult_ViewName_Ok()
        {
            var result = await Task.FromResult(_controller.ForgotPassword());
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewName == "ForgotPassword");
        }

        [Fact]
        public async Task ForgotPassword_UserNotFound()
        {
            _userManager.Setup(x => x.GetUserEmailAsync("email@gmail.com"))
                .ReturnsAsync(null as UserDto);

            var result = await _controller.ForgotPassword(new ForgotPasswordViewModel());

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }


        //[Fact]
        //public async Task ForgotPassword_Model_Ok()
        //{
        //    _userManager.Setup(x => x.GetUserEmailAsync("email@gmail.com"))
        //        .ReturnsAsync(new UserDto { Id = 1});

        //    var code = _userManager.Setup(x => x.GeneratePasswordResetTokenAsync(1))
        //        .ReturnsAsync("code");

        //    var result = await _controller
        //        .ForgotPassword(new ForgotPasswordViewModel { Email = "email@gmail.com" });

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //}
    }
}