using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Tests.Fakes;
using BlogPost.Web.Controllers;
using BlogPost.Web.Infrastructure;
using BlogPost.Web.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Tests
{
    public class PostsControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostManager> _postManager;
        private readonly PostsController _controller;

        public PostsControllerTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _postManager = new Mock<IPostManager>();
            _controller = new PostsController(_mapper, _postManager.Object);
        }

        [Fact]
        public async Task CreatePost_ViewName_Ok()
        {
            var result = await Task.FromResult(_controller.CreatePost(1));
            
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreatePostViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task ConfirmCreateBlog_ViewName_CreateBlog_Ok()
        {
            _controller.SetupUser(1);
            _postManager.Setup(repo => repo.CreatePost(new PostDto()));
            var result = await _controller.ConfirmCreatePost(new CreatePostViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
        }

        [Fact]
        public async Task PostDetails_GetPost_Model_ViewName_Ok()
        {
            var postgDto = _postManager.Setup(repo => repo.GetPost(1))
                .ReturnsAsync(new PostDto());

            var result = await _controller.PostDetails(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<PostDetailsViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName == "PostDetails");
        }

        [Fact]
        public async Task PostDetails_PostIsNull()
        {
            var postgDto = _postManager.Setup(repo => repo.GetPost(1))
                .ReturnsAsync(null as PostDto);

            var result = await _controller.PostDetails(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPost_GetPost_Model_Ok()
        {
            var postgDto = _postManager.Setup(repo => repo.GetPost(1))
                .ReturnsAsync(new PostDto());

            var result = await _controller
                .EditPost(new UpdatePostViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UpdatePostViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task EditPost_PostIsNull()
        {
            var postgDto = _postManager.Setup(repo => repo.GetPost(1))
                .ReturnsAsync(null as PostDto);

            var result = await _controller
                .EditPost(new UpdatePostViewModel { Id = 1 });

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmEditPost_ViewName_Ok()
        {
            _controller.SetupUser(1);
            var result = await _controller.ConfirmEditPost(new UpdatePostViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
        }

        [Fact]
        public async Task ConfirmEditPost_GetPost_Ok()
        {
            _postManager.Setup(repo => repo.UpdatePost(new PostDto()));
            _controller.SetupUser(1);

            var result = await _controller
                .ConfirmEditPost(new UpdatePostViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task ConfirmEditPost_UpdateModel_Ok()
        {
            var model = new UpdatePostViewModel { Id = 1, Title = "Test" };
            var user = _controller.SetupUser(1);

            await _controller.ConfirmEditPost(model);
            _postManager.Verify(repo => repo.UpdatePost(It.Is<PostDto>(blog =>
                blog.Title == "Test" && blog.Id == 1 && blog.User.Id == user.Id)), Times.Once);
        }

        [Fact]
        public async Task DeletePost_GetPost_Model_Ok()
        {
            var postgDto = _postManager.Setup(repo => repo.GetPost(1))
                .ReturnsAsync(new PostDto());

            var result = await _controller
                .DeletePost(new PostViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PostViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task DeletePost_PostIsNull()
        {
            var postgDto = _postManager.Setup(repo => repo.GetPost(1))
                .ReturnsAsync(null as PostDto);

            var result = await _controller
                .DeletePost(new PostViewModel());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmDeletePost_Ok()
        {
            var post = new PostViewModel { Id = 1 };
            await _controller.ConfirmDeletePost(post);
            _postManager.Verify(repo => repo.DeletePost(1), Times.Once);

            _postManager.Verify(repo =>
                repo.DeletePost(It.Is<int>(id => id == post.Id)));
        }
    }
}
