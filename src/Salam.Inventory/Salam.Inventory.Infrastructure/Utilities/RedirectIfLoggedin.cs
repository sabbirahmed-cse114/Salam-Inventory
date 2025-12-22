using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Salam.Inventory.Infrastructure.Utilities
{
    public class RedirectIfLoggedin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if(user.Identity?.IsAuthenticated == true)
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", new {area ="Admin"});
            }
            base.OnActionExecuting(context);
        }
    }
}