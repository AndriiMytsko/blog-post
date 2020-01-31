using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Controllers;
using BlogPost.Web.Infrastructure;
using BlogPost.Web.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Tests
{
    public class HomeControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostManager> _postManager;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _postManager = new Mock<IPostManager>();
            _controller = new HomeController(_mapper, _postManager.Object);
        }

        [Fact]
        public async Task ListPosts_ViewName_Ok()
        {
            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewName == "Index");
        }

        [Fact]
        public async Task ListPosts_Model_Ok()
        {
           _postManager.Setup(repo => repo.GetLastPosts()).Returns(GetTestPosts());

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<List<PostViewModel>>(viewResult.Model);
            Assert.True(models.Count == 4);
        }

        private async Task<IList<PostDto>> GetTestPosts()
        {
            var posts = new List<PostDto>
            {
                new PostDto { Id=1, Title="Tom"},
                new PostDto { Id=2, Title="Alice"},
                new PostDto { Id=3, Title="Sam"},
                new PostDto { Id=4, Title="Kate"}
            };
            
            return await Task.FromResult(posts);
        }
    }
}
