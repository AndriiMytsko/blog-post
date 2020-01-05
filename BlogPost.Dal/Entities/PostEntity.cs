using BlogPost.Dal.Identities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class PostEntity : Entity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public int BlogId { get; set; }

        public int? UserId { get; set; }

        [ForeignKey(nameof(BlogId))]
        public BlogEntity Blog { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}