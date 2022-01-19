using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers
{
    public class PerfectWorldFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            #region Authentication
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated) { filterContext.Result = new RedirectToActionResult("Login", "Log", null); return; }
            #endregion
        }


    }
}
