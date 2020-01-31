using System.ComponentModel.DataAnnotations;

namespace BlogPost.Web.Models.Roles
{
    public class RoleViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
