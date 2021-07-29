using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Common
{
    public interface IapiPlugin
    {
        void pagePublish(string message, string postUrl);
    }
}
