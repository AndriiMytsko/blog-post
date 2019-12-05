using System;

namespace BlogPost.Web.Models.Comments
{
    public class CommentDetailsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
