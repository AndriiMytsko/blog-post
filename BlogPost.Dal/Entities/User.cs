using Microsoft.AspNetCore.Identity;

namespace BlogPost.Dal.Entities
{
    public class User : IdentityUser
    {
        public UserProfile UserProfile { get; set; }
    }
}
