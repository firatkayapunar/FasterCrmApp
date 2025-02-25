using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface INotificationService
    {
        Result<List<NotificationModel>> GetListByUserId(int userId, bool? isRead);
        Result<List<NotificationModel>> ListBySearch(string search, int userId);
        Result Create(CreateNotificationModel createNotificationModel);
        Result Edit(EditNotificationModel editNotificationModel);
    }
}
