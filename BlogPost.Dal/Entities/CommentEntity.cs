using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class CommentEntity : Entity
    {
        public string Text { get; set; }
        public PostEntity PostEntity { get; set; }

        [ForeignKey("PostEntity")]
        public int PostId { get; set; }
    }
}