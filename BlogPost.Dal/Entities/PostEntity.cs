using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class PostEntity : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public BlogEntity Blog { get; set; }

        [ForeignKey(nameof(BlogEntity))]
        public int BlogId { get; set; }

        public int UserId { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}
