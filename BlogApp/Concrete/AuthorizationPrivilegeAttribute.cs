using BlogApp.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public class AuthorizationPrivilegeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var principal = filterContext.HttpContext.User as ClaimsPrincipal;
            #region comment
            if (!principal.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/user/login");
                return;
            }
            #endregion
            if (!string.IsNullOrEmpty(ClaimValue))
            {
                var claimValue = ClaimValue.Split(',');
                if (!(principal.HasClaim(x => x.Type == ClaimType && claimValue.Any(v => v == x.Value) && x.Issuer == Constants.Issuer)))
                {
                    filterContext.Result = new RedirectResult("~/Views/Error/AccessDenied.html");
                }
            }
            else
            {
                if (filterContext.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(AllowAnonymousAttribute)))
                {
                    return;
                }
                if (!principal.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Views/Error/AccessDenied.html");
                }
            }

        }
    }
}
