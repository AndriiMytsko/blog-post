using BlogPost.Dal.Identities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Entities
{
    public class BlogEntity : Entity
    {
        public string Title { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}