using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Tests.Fakes;
using BlogPost.Web.Controllers;
using BlogPost.Web.Infrastructure;
using BlogPost.Web.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Tests
{
    public class CommentsControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICommentManager> _commentManager;
        private readonly CommentsController _controller;

        public CommentsControllerTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _commentManager = new Mock<ICommentManager>();
            _controller = new CommentsController(_mapper, _commentManager.Object);
        }

        [Fact]
        public async Task CreateComment_Model_ViewName_Ok()
        {
            var result = await Task.FromResult(_controller.CreateComment(1));

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateCommentViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName == "CreateComment");
        }

        [Fact]
        public async Task ConfirmCreateComment_CreateComment_Ok()
        {
            _controller.SetupUser(1);
            _commentManager.Setup(repo => repo.CreateComment(new CommentDto()));
            var result = await _controller.ConfirmCreateComment(new CreateCommentViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
        }

        [Fact]
        public async Task EditComment_GetComment_Model_Ok()
        {
            var commentDto = _commentManager.Setup(repo => repo.GetComment(1))
                .ReturnsAsync(new CommentDto());

            var result = await _controller
                .EditComment(new UpdateCommentViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UpdateCommentViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task EditComment_CommentIsNull()
        {
            var commentDto = _commentManager.Setup(repo => repo.GetComment(1))
                .ReturnsAsync(null as CommentDto);

            var result = await _controller
                .EditComment(new UpdateCommentViewModel { Id = 1 });

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmEditComment_ViewName_Ok()
        {
            _controller.SetupUser(1);
            var result = await _controller.ConfirmEditComment(new UpdateCommentViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.True(viewResult.ActionName == "Index");
        }

        [Fact]
        public async Task ConfirmEditComment_GetComment_Ok()
        {
            _commentManager.Setup(repo => repo.UpdateComment(new CommentDto()));
            _controller.SetupUser(1);

            var result = await _controller
                .ConfirmEditComment(new UpdateCommentViewModel());

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task ConfirmEditComment_UpdateModel_Ok()
        {
            var model = new UpdateCommentViewModel { Id = 1, Text = "Test" };
            var user = _controller.SetupUser(1);

            await _controller.ConfirmEditComment(model);

            _commentManager.Verify(repo => repo.UpdateComment(It.Is<CommentDto>(com =>
               com.Text == "Test" && com.Id == 1 && com.User.Id == user.Id)), Times.Once);
        }

        [Fact]
        public async Task DeleteComment_GetComment_Model_Ok()
        {
            var commentDto = _commentManager.Setup(repo => repo.GetComment(1))
                .ReturnsAsync(new CommentDto());

            var result = await _controller
                .DeleteComment(new CommentViewModel { Id = 1 });

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CommentViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task DeleteComment_CommentIsNull()
        {
            var commentDto = _commentManager.Setup(repo => repo.GetComment(1))
                .ReturnsAsync(null as CommentDto);

            var result = await _controller
                .DeleteComment(new CommentViewModel());

            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmDeleteComment_Ok()
        {
            var comment = new CommentViewModel { Id = 1 };
            await _controller.ConfirmDeleteComment(comment);

            _commentManager.Verify(repo => repo.DeleteComment(1), Times.Once);
            _commentManager.Verify(repo => 
                repo.DeleteComment(It.Is<int>(id => id == comment.Id)));
        }
    }
}