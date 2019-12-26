using AutoMapper;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Web.Components.Posts
{
    public class PostsViewComponent : ViewComponent
    {
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;

        public PostsViewComponent(IPostManager postManager, IMapper mapper)
        {
            _postManager = postManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int blogId)
        {
            var posts = await _postManager.GetAllPosts(blogId);
            var models = _mapper.Map<IList<PostDetailsViewModel>>(posts);

            return View("Posts", models);
        }
    }
}
