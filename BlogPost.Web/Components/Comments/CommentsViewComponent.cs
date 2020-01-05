using AutoMapper;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Web.Components.Posts
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentManager _commentManager;
        private readonly IMapper _mapper;

        public CommentsViewComponent(ICommentManager commentManaget, IMapper mapper)
        {
            _commentManager = commentManaget;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var comments = await _commentManager.GetComments(postId);
            var models = _mapper.Map<IList<CommentViewModel>>(comments);

            return View("Comments", models);
        }
    }
}
