using AgreementManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementManagement.Web.Controllers
{
    public class AuhtActionFilterAttribute : ActionFilterAttribute
    {
        public AuhtActionFilterAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                var values = new RouteValueDictionary(new
                {
                    area = "",
                    action = "Index",
                    controller = "Home",
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }
    }
}