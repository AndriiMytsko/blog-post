using BlogPost.Dal.Identities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class PostEntity : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }

        [ForeignKey("BlogId")]
        public virtual BlogEntity Blog { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}