using BlogPost.Bll.DTOs;
using System;
using System.Collections.Generic;

namespace BlogPost.Web.Models.Comments
{
    public class CommentDetailsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
