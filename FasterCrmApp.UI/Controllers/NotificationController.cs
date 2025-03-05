using FasterCrmApp.Common;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.UI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    [Auth]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public ActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);
            var result = _notificationService.GetListByUserId(userId ?? 0, null);

            return View(result);
        }

        // GET: Notification/Search?search=value
        [HttpGet("Notification/Search")]
        public IActionResult Search(string search)
        {
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);

            Result<List<NotificationModel>> result;

            if (string.IsNullOrWhiteSpace(search))
                result = _notificationService.GetListByUserId(userId ?? 0, false);
            else
                result = _notificationService.ListBySearch(search, userId ?? 0);

            return ReturnResult(result);
        }

        // POST: Issue/Update/{id}
        [HttpPost("Notification/Update/{id}")]
        public IActionResult Update(EditNotificationModel editNotificationModel)
        {
            var result = _notificationService.Edit(editNotificationModel);
            return ReturnResult(result);
        }
    }
}
