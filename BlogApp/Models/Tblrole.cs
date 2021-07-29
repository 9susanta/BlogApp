using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Tblrole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
