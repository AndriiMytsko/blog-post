using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Web.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommentManager _commentManager;

        public CommentsController(IMapper mapper, ICommentManager commentManager)
        {
            _mapper = mapper;
            _commentManager = commentManager;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _commentManager.GetAllComments();
            var models = _mapper.Map<IList<CommentViewModel>>(comments);

            return View(models);
        }

        public IActionResult CreateComment(int postId)
        {
            return View(new CreateCommentViewModel { PostId = postId });
        }

        public async Task<IActionResult> ConfirmCreateComment(CreateCommentViewModel comment)
        {
            var commentDto = _mapper.Map<CommentDto>(comment);
            await _commentManager.CreateComment(commentDto);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditComment(UpdateCommentViewModel comment)
        {
            var commentDto = await _commentManager.GetComment(comment.Id);
            var commentModel = _mapper.Map<UpdateCommentViewModel>(commentDto);

            return View(commentModel);
        }

        public async Task<IActionResult> ConfirmEditComment(UpdateCommentViewModel comment)
        {
            var commentDto = _mapper.Map<CommentDto>(comment);
            await _commentManager.UpdateComment(commentDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteComment(CommentViewModel comment)
        {
            var commentDto = await _commentManager.GetComment(comment.Id);
            var commentModel = _mapper.Map<CommentViewModel>(commentDto);

            return View(commentModel);
        }

        public async Task<IActionResult> ConfirmDeleteComment(CommentViewModel comment)
        {
            await _commentManager.DeleteComment(comment.Id);

            return RedirectToAction("Index");
        }
    }
}