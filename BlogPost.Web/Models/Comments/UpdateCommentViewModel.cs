using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Web.Models.Comments
{
    public class UpdateCommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
