using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
