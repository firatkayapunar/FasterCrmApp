using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
