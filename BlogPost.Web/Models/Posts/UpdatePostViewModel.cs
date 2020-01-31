using System;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Posts
{
    public class UpdatePostViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
    }
}