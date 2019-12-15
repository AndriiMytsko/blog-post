using BlogPost.Dal.Entities;
using System.Collections.Generic;

namespace BlogPost.Bll.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }

        public IEnumerable<PostEntity> Posts { get; set; }
    }
}
