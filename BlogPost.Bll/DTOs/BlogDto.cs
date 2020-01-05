using System;

namespace BlogPost.Bll.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto User { get; set; }
    }
}
