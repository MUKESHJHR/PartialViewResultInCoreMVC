using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Primitives;

namespace PartialViewResultInCoreMVC.Models
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (routeContext.HttpContext.Request.Headers != null && routeContext.HttpContext.Request.Headers.ContainsKey("X-Requested-With") && routeContext.HttpContext.Request.Headers.TryGetValue("X-Requested-With", out StringValues requestedWithHeader))
            {
                if (requestedWithHeader.Contains("XMLHttpRequest"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

