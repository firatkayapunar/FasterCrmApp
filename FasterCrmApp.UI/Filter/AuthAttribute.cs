using FasterCrmApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FasterCrmApp.UI.Filter
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; } = string.Empty;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains(Constants.Session_ID))
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    var role = context.HttpContext.Session.GetString(Constants.Session_RoleName).ToLower();
                    string[] roles = Roles.Split(",");  // admin, user

                    if (!roles.Contains(role))
                    {
                        context.Result = new RedirectResult("/");
                        return;
                    }
                }
            }
        }
    }
}
