using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class CommentEntity : Entity
    {
        public string Text { get; set; }
        public BlogEntity BlogEntity { get; set; }

        [ForeignKey("BlogEntity")]
        public int BlogId { get; set; }
    }
}