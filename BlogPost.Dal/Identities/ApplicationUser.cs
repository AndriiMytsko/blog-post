using BlogPost.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPost.Dal.Identities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? ProfileImageId { get; set; }

        [ForeignKey(nameof(ProfileImageId))]
        public virtual ImageEntity ProfileImage { get; set; }
    }
}
