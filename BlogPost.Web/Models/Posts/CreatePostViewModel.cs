using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Posts
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public int BlogId { get; set; }
    }
}
