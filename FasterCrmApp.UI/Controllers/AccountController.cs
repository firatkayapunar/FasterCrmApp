using FasterCrmApp.Common;
using FasterCrmApp.Entities.Enum;
using FasterCrmApp.Models;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost("Login")]
        public IActionResult Login(AuthenticateModel authenticateModel)
        {
            var result = _userService.Authenticate(authenticateModel);

            if (result.Data != null)
            {
                // Kullanıcının adını session'a ekliyoruz.
                HttpContext.Session.SetString(Constants.Session_Name, result.Data.Name);

                // Kullanıcının rolü int olarak geliyor, enum'a dönüştürüyoruz.
                var roleEnum = (Role)result.Data.Role;

                // RoleHelper'dan rolün string karşılığını alıyoruz.
                var roleName = RoleHelper.GetRoleName(roleEnum);

                // Rolün string karşılığını session'a ekliyoruz.
                HttpContext.Session.SetString(Constants.Session_Role, roleName);

                // Kullanıcının ID'sini session'a ekliyoruz.
                HttpContext.Session.SetString(Constants.Session_ID, result.Data.ID.ToString());
            }

            return ReturnResult(result);
        }

        public IActionResult CreateFakeUser()
        {
            _userService.Add(new CreateUserModel
            {
                Name = "Test",
                Email = "test.firat@gmail.com",
                Password = "12345",
                Locked = false,
                RePassword = "12345",
                Role = 1,
                Username = "Test",
            });

            return Ok();
        }
    }
}
