using FasterCrmApp.Common;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly INotificationService _notificationService;

        public NavbarViewComponent(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IViewComponentResult Invoke()
        {
            var session = HttpContext.Session;

            var userId = HttpContext.Session.GetInt32(Constants.Session_ID) ?? 0;

            var unreadNotifications = _notificationService.GetListByUserId(userId, false)
                                      .Data
                                      .Take(5)
                                      .ToList();

            if (unreadNotifications.Count < 5)
            {
                var readNotifications = _notificationService.GetListByUserId(userId, true)
                                        .Data
                                        .Take(5 - unreadNotifications.Count)
                                        .ToList();

                // C#'ta List<T> koleksiyonlarında kullanılan AddRange() metodu, başka bir listeyi (veya koleksiyonunu) mevcut listeye toplu olarak eklemek için kullanılır.
                unreadNotifications.AddRange(readNotifications);
            }

            var notifications = unreadNotifications;
            var model = new NavbarViewModel
            {
                Notifications = notifications
            };

            return View(model);
        }
    }
}
