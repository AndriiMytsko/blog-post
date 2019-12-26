using System;

namespace BlogPost.Bll.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public PostDto Post { get; set; }
        public UserDto User { get; set; }

    }
}