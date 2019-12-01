using System.Collections.Generic;

namespace BlogPost.Dal.Entities
{
    public class BlogEntity : Entity
    {
        public string Title { get; set; }

        public virtual IEnumerable<PostEntity> Posts { get; set; }
    }
}
