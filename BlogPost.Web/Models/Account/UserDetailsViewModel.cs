using Microsoft.AspNetCore.Http;

namespace BlogPost.Web.Models.Account
{
    public class UserDetailsViewModel
    {
        public string UserName { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
