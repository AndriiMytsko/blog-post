using System;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Blogs
{
    public class UpdateBlogViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
