using System;

namespace BlogPost.Web.Models.Blogs
{
    public class UpdateBlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
