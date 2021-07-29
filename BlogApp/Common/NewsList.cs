using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Common
{
    public class NewsList
    {
        public List<ClsPost> news { get; set; }
        public int totalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPages { get; set; }
    }
}
