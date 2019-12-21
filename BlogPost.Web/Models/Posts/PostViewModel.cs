using BlogPost.Bll.DTOs;
using System;

namespace BlogPost.Web.Models.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
