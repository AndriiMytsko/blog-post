using BlogPost.Dal.Identities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class CommentEntity : Entity
    {
        public string Text { get; set; }

        [ForeignKey("PostId")]
        public virtual PostEntity Post { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}