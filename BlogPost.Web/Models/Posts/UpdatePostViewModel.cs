using System;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Posts
{
    public class UpdatePostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}