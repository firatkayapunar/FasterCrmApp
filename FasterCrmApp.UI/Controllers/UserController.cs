using FasterCrmApp.Common;
using FasterCrmApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var result = _userService.GetList(Convert.ToInt16(HttpContext.Session.GetString(Constants.Session_ID)));
            return View(result);
        }
    }
}
