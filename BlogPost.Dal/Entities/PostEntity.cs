using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogPost.Dal.Entities
{
    public class PostEntity : Entity
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public BlogEntity BlogEntity { get; set; }

        [ForeignKey("BlogEntity")]
        public int BlogId { get; set; }

        public virtual IEnumerable<CommentEntity> Comments { get; set; }
    }
}
