using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Tbluser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public string ImageName { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int? NoofFailurAttempt { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
