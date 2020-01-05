using System;

namespace BlogPost.Bll.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public BlogDto Blog { get; set; }
        public UserDto User { get; set; }
    }
}