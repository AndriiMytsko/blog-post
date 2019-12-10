using BlogPost.Dal.Identities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class PostEntity : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public int? BlogId { get; set; }
        [ForeignKey(nameof(BlogId))]
        public virtual BlogEntity Blog { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}
