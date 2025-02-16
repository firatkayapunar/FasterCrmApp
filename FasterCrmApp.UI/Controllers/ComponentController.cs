using FasterCrmApp.UI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    [SessionCheck("Login", "Account")]
    public class ComponentController : Controller
    {
        public IActionResult Navbar()
        {
            return ViewComponent("Navbar");
        }
    }
}
