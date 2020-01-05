using BlogPost.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogPost.Dal.Identities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? ProfileImageId { get; set; }

        public ImageEntity ProfileImage { get; set; }

        public virtual ICollection<CommentEntity> Comments { get; set; }

        public virtual ICollection<PostEntity> Posts { get; set; }
    }
}
