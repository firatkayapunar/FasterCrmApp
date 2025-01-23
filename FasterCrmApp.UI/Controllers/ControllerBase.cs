using FasterCrmApp.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class ControllerBase : Controller
    {
        protected ActionResult ReturnResult(Result result)
        {
            return Json(result);
        }
    }
}
