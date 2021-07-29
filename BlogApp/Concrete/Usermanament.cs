using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public class Usermanament: IUsermanament
    {
        private readonly blogdbContext _context;
        public Usermanament(blogdbContext context)
        {
            _context = context;
        }
        public clsUsers Login(string UserName, string Password)
        {
            try
            {

                    var loggeduser = (from user in _context.Tblusers
                                      join rol in _context.Tblroles on user.RoleId equals rol.RoleId
                                      where user.UserName.ToLower() == UserName.ToLower() && user.Password == Password && (user.IsDeleted == false)
                                      select new { user.UserName, user.FullName, rol.RoleName, user.UserId }).ToList();


                    if (loggeduser.Count > 0)
                    {
                        return new clsUsers
                        {
                            FullName = loggeduser.FirstOrDefault().FullName,
                            UserName = loggeduser.FirstOrDefault().UserName,
                            RoleName = loggeduser.FirstOrDefault().RoleName,
                            UserId = loggeduser.FirstOrDefault().UserId
                        };
                    }
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }
    }
}
