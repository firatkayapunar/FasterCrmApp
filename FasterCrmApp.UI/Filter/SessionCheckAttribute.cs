using FasterCrmApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FasterCrmApp.UI.Filter
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        private readonly string _redirectAction;
        private readonly string _redirectController;

        public SessionCheckAttribute(string redirectAction, string redirectController)
        {
            _redirectAction = redirectAction;
            _redirectController = redirectController;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Eğer session'da belirli bir değer yoksa yönlendirme yap dedim.
            if (string.IsNullOrWhiteSpace(context.HttpContext.Session.GetString(Constants.Session_ID)))
            {
                context.Result = new RedirectToActionResult(
                    _redirectAction,
                    _redirectController,
                    null
                );
            }

            base.OnActionExecuting(context);
        }
    }
}
