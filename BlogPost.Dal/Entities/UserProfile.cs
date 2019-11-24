using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Dal.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
    }
}
