﻿using FasterCrmApp.Common;
using FasterCrmApp.Entities.Enum;
using FasterCrmApp.Models;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Concrete;
using FasterCrmApp.UI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public AccountController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost("Account/Login")]
        public IActionResult Login(AuthenticateModel authenticateModel)
        {
            var result = _userService.Authenticate(authenticateModel);

            if (result.Data != null)
            {
                // Kullanıcının adını session'a ekliyoruz.
                HttpContext.Session.SetString(Constants.Session_Name, result.Data.Name);

                // Rolün int karşılığını session'a ekliyoruz.
                HttpContext.Session.SetInt32(Constants.Session_RoleValue, result.Data.Role);

                // Kullanıcının rolü int olarak geliyor, enum'a dönüştürüyoruz.
                var roleEnum = (Role)result.Data.Role;

                // RoleHelper'dan rolün string karşılığını alıyoruz.
                var roleName = RoleHelper.GetRoleName(roleEnum);

                // Rolün string karşılığını session'a ekliyoruz.
                HttpContext.Session.SetString(Constants.Session_RoleName, roleName);

                // Kullanıcının ID'sini session'a ekliyoruz.
                HttpContext.Session.SetInt32(Constants.Session_ID, result.Data.ID); 
            }

            return ReturnResult(result);
        }

        [SessionCheck("Login", "Account")]
        public IActionResult Logout()
        {
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID) ?? 0;

            var logResult = _logService.Create(new CreateLogModel
            {
                Text = "Sistem çıkışı yapıldı.",
                LogType = (int)LogType.SystemLogout,
                UserID = userId
            });

            if (!logResult.IsSuccess) 
            {
                TempData["ErrorMessage"] = "Çıkış işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.";
                return RedirectToAction("Dashboard", "Home");
            }

            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Login));
        }
    }
}
