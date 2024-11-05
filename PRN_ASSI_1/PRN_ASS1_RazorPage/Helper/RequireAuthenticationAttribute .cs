using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace PRN_ASS1_RazorPage.Helper
{
    // Filters/AuthenticationFilter.cs
    public class RequireAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToPageResult("/Index");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
