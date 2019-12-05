using BlogPost.Dal.Identities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class BlogEntity : Entity
    {
        public string Title { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int UserId { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}
