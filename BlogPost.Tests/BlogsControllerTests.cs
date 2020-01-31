using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Tests.Fakes;
using BlogPost.Web.Controllers;
using BlogPost.Web.Infrastructure;
using BlogPost.Web.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Tests
{
    public class BlogsControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBlogManager> _blogManager;
        private readonly BlogsController _controller;

        public BlogsControllerTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _blogManager = new Mock<IBlogManager>();
            _controller = new BlogsController(_mapper, _blogManager.Object);
        }

        [Fact]
        public async Task CreateBlog_ViewName_Ok()
        {
            var result = await Task.FromResult(_controller.CreateBlog());

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewName == "CreateBlog");
        }

        [Fact]
        public async Task ConfirmCreateBlog_CreateBlog_Ok()
        {
            var user = _controller.SetupUser(1);
            _blogManager.Setup(repo => repo.CreateBlog(new BlogDto()));
            var result = await _controller.ConfirmCreateBlog(new CreateBlogViewModel());
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            _blogManager.Verify(x => x.CreateBlog(It.IsAny<BlogDto>()));
        }

        [Fact]
        public async Task BlogDetails_GetBlog__Model_ViewName_Ok()
        {
            var blogDto = _blogManager.Setup(repo => repo.GetBlog(1))
                .ReturnsAsync(new BlogDto());

            var result = await _controller.BlogDetails(new BlogViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<BlogViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName == "BlogDetails");
        }

        [Fact]
        public async Task BlogDetails_BlogIsNull()
        {
            var blogDto = _blogManager.Setup(repo => repo.GetBlog(1))
                .ReturnsAsync(null as BlogDto);

            var result = await _controller.BlogDetails(new BlogViewModel { Id = 1 });

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditBlog_GetBlog__Model_ViewName_Ok()
        {
            var blogDto = _blogManager.Setup(repo => repo.GetBlog(1))
                .ReturnsAsync(new BlogDto());

            var result = await _controller
                .EditBlog(new UpdateBlogViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UpdateBlogViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task EditBlog_BlogIsNull()
        {
            var blogDto = _blogManager.Setup(repo => repo.GetBlog(1))
                .ReturnsAsync(null as BlogDto);

            var result = await _controller
                .EditBlog(new UpdateBlogViewModel { Id = 1 });

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmEditBlog_ViewName_Ok()
        {
            _controller.SetupUser(1);
            var result = await _controller.ConfirmEditBlog(new UpdateBlogViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
            Assert.True(viewResult.ControllerName == "Home");
        }

        [Fact]
        public async Task ConfirmEditBlog_UpdateBlog_Ok()
        {
            _blogManager.Setup(repo => repo.UpdateBlog(new BlogDto()));
            var user = _controller.SetupUser(1);

            var result = await _controller
                .ConfirmEditBlog(new UpdateBlogViewModel());

           Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task ConfirmEditBlog_Model_UpdateBlog_Ok()
        {
            var model = new UpdateBlogViewModel { Id = 1, Title = "Test" };

            var user = _controller.SetupUser(1);
            await _controller.ConfirmEditBlog(model);
            _blogManager.Verify(repo => repo.UpdateBlog(It.Is<BlogDto>(blog =>
                blog.Title == "Test" && blog.Id == 1 && blog.User.Id == user.Id)), Times.Once);
        }

        [Fact]
        public async Task DeleteBlog_GetBlog_Model_ViewName_Ok()
        {
            var blogDto = _blogManager.Setup(repo => repo.GetBlog(1))
                .ReturnsAsync(new BlogDto());

            var result = await _controller
                .DeleteBlog(new BlogViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<BlogViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task DeleteBlog_BlogIsNull()
        {
            var blogDto = _blogManager.Setup(repo => repo.GetBlog(1))
                .ReturnsAsync(null as BlogDto);

            var result = await _controller
                .DeleteBlog(new BlogViewModel());

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmDeleteBlod_Ok()
        {
            var blog = new BlogViewModel { Id = 1 };

            await _controller.ConfirmDeleteBlog(blog);
            _blogManager.Verify(repo => repo.DeleteBlog(1), Times.Once);
        }
    }
}
