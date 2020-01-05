using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly ICommentManager _commentManager;
        private readonly IUserManager _userManager;

        public CommentsController(
            IMapper mapper,
            ICommentManager commentManager,
            IUserManager userManager)
            : base(mapper)
        {
            _commentManager = commentManager;
            _userManager = userManager;
        }

        public IActionResult CreateComment(int postId)
        {
            var commentModel = new CreateCommentViewModel
            {
                PostId = postId
            };

            return View(commentModel);
        }

        public async Task<IActionResult> ConfirmCreateComment(CreateCommentViewModel comment)
        {
            var commentDto = Mapper.Map<CommentDto>(comment);
            commentDto.User = User.CreateUserDto();

            await _commentManager.CreateComment(commentDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditComment(UpdateCommentViewModel comment)
        {
            var commentDto = await _commentManager.GetComment(comment.Id);
            var commentModel = Mapper.Map<UpdateCommentViewModel>(commentDto);

            return View(commentModel);
        }

        public async Task<IActionResult> ConfirmEditComment(UpdateCommentViewModel comment)
        {
            var commentDto = Mapper.Map<CommentDto>(comment);
            await _commentManager.UpdateComment(commentDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteComment(CommentViewModel comment)
        {
            var commentDto = await _commentManager.GetComment(comment.Id);
            var commentModel = Mapper.Map<CommentViewModel>(commentDto);

            return View(commentModel);
        }

        public async Task<IActionResult> ConfirmDeleteComment(CommentViewModel comment)
        {
            await _commentManager.DeleteComment(comment.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}