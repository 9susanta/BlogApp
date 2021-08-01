using BlogApp.Common;
using BlogApp.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    [AuthorizationPrivilegeAttribute]
    public class BaseController : Controller
    {
        protected internal UserSessionModel UserSessionModel { get; private set; }

        public BaseController()
        {
            var user = User as ClaimsPrincipal;
            if (user != null)
            {
                var claims = user.Claims.ToList();
                var sessionClaim = claims.FirstOrDefault(o => o.Type == Constants.UserSession);
                if (sessionClaim != null)
                {
                    UserSessionModel = sessionClaim.Value.ToObject<UserSessionModel>();
                }
            }
        }
    }
}
