using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public interface IUsermanament
    {
        clsUsers Login(string UserName, string Password);
    }
}
