using BlogPost.Dal.Identities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class CommentEntity : Entity
    {
        public string Text { get; set; }
        public PostEntity Post { get; set; }

        [ForeignKey(nameof(PostEntity))]
        public int PostId { get; set; }

        public int UserId { get; set; }
    }
}