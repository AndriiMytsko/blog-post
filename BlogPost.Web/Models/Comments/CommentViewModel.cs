using BlogPost.Bll.DTOs;
using System;

namespace BlogPost.Web.Models.Comments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
