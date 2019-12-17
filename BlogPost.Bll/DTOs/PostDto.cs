using BlogPost.Dal.Entities;
using System;
using System.Collections.Generic;

namespace BlogPost.Bll.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BlogId { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }


        public IList<CommentEntity> Comments { get; set; }
    }
}