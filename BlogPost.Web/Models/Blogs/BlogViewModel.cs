using BlogPost.Bll.DTOs;
using System;
using System.Collections.Generic;

namespace BlogPost.Web.Models.Blogs
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
    }
}