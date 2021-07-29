using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Tblnewstype
    {
        public int Id { get; set; }
        public string OdiaName { get; set; }
        public string NewsType { get; set; }
        public string Description { get; set; }
        public bool? IsMenu { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
