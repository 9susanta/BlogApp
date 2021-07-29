using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public class clsNewsType:IclsNewsType
    {
        private readonly blogdbContext _context;
        public clsNewsType(blogdbContext context)
        {
            _context = context;
        }
        public int CheckNewsType(string NewsType)
        {
            try
            {
                var items = _context.Tblnewstypes.Where(x => x.NewsType == NewsType).ToList().Count;
                return items;   
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
    }
}
