using BlogPost.Bll.DTOs;
using System;
using System.Collections.Generic;

namespace BlogPost.Web.Models.Posts
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
    }
}