using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlogPost.Dal.Identities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public byte[] ProfileImage { get; set; }
    }
}
