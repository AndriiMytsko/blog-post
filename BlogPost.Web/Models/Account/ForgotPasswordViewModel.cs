using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
