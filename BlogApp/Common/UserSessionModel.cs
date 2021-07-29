using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Common
{
    public class UserSessionModel
    {
        public Guid UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
