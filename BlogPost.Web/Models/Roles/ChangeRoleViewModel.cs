using System.Collections.Generic;

namespace BlogPost.Web.Models.Roles
{
    public class ChangeRoleViewModel
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public IList<RoleViewModel> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<RoleViewModel>();
            UserRoles = new List<string>();
        }
    }
}
