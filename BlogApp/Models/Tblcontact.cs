using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Tblcontact
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? PostedOn { get; set; }
    }
}
