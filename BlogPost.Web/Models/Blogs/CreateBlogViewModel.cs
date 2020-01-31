using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Blogs
{
    public class CreateBlogViewModel
    {
        [Required]
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}
