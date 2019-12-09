using BlogPost.Dal.Identities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class CommentEntity : Entity
    {
        public string Text { get; set; }

        public int? PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public PostEntity Post { get; set; }


        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

    }
}