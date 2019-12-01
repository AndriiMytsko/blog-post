using System;

namespace BlogPost.Web.Models.Posts
{
    public class UpdatePostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}