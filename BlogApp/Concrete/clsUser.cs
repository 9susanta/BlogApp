using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public class clsUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage ="Please Enter User")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string ImageName { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
