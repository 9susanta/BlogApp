using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Tblright
    {
        public int RightsId { get; set; }
        public string RightsName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
