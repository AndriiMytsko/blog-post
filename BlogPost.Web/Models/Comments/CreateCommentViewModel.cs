using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Comments
{
    public class CreateCommentViewModel
    {
        [Required]
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
