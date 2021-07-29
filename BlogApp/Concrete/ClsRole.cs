using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public class ClsRole:IClsRole
    {
        private readonly blogdbContext _context;
        public ClsRole(blogdbContext context)
        {
            _context = context;
        }
        public int CheckRoleName(string RoleName)
        {
            try
            {
                var items = _context.Tblroles.Where(x => x.RoleName == RoleName).ToList().Count;
                return items;
                
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
    }
}
