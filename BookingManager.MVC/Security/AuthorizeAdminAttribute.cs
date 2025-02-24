using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingManager.MVC.Security
{
    public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? role = context.HttpContext.Session.GetString("ROLE");
            if (role != "Customer")
            {
                context.HttpContext.Response.Redirect("/Customer/Login");
            }
        }
    }
}
