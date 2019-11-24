using System.Collections.Generic;

namespace BlogPost.Bll.DTOs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CommentDto> Comments { get; set; }
    }
}
