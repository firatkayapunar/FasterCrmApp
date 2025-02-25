using AutoMapper;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities;
using FasterCrmApp.Entities.Enum;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Validation;
using FasterCrmApp.Services.Validation.FluentValidation;

namespace FasterCrmApp.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public Result<List<NotificationModel>> GetListByUserId(int userId, bool? isRead)
         {
            try
            {
                IEnumerable<Notification> existingClient;

                if (isRead == null)
                    existingClient = _notificationRepository.GetAll(x => x.UserID == userId);
                else
                    existingClient = _notificationRepository.GetAll(x => x.UserID == userId && x.IsRead == isRead);

                if (existingClient == null || !existingClient.Any())
                    return Result<List<NotificationModel>>.SuccessResult(new List<NotificationModel>(), "No notifications found.");

                var notificationModels = _mapper.Map<List<NotificationModel>>(existingClient);

                return Result<List<NotificationModel>>.SuccessResult(notificationModels.OrderByDescending(x => x.CreatedAt).ToList(), "Notifications retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<NotificationModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<NotificationModel>> ListBySearch(string search, int userId)
        {
            try
            {
                var notifications = _notificationRepository.GetAll(x =>
                                    x.UserID == userId
                                    &&
                                    x.Text.Contains(search)
                );

                if (notifications == null || !notifications.Any())
                    return Result<List<NotificationModel>>.FailureResult("No notifications found.", new List<string> { "The database contains no notifications." });

                var notificationModels = _mapper.Map<List<NotificationModel>>(notifications);

                return Result<List<NotificationModel>>.SuccessResult(notificationModels.OrderByDescending(x => x.CreatedAt).ToList(), "Notifys retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<NotificationModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result Create(CreateNotificationModel createNotifyModel)
        {
            try
            {
                var notification = _mapper.Map<Notification>(createNotifyModel);
                notification.NotificationType = (NotificationType)createNotifyModel.NotifcationType;
                ValidationTool.Validate(new NotificationValidator(), notification);

                _notificationRepository.Add(notification);

                return Result.SuccessResult("Notify successfully added.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };

                return Result.FailureResult("An error occurred while adding the notification.", errors);
            }
        }

        public Result Edit(EditNotificationModel editNotifyModel)
        {
            try
            {
                var existingNotify = _notificationRepository.GetById(editNotifyModel.ID);

                if (existingNotify == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The notification with the given ID does not exist." } }
                    };

                    return Result.FailureResult("Notify not found.", errors);
                }

                var notification = _mapper.Map(editNotifyModel, existingNotify);

                ValidationTool.Validate(new NotificationValidator(), notification);

                _notificationRepository.Update(notification);

                return Result.SuccessResult("Notify successfully updated.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };

                return Result.FailureResult("An error occurred while updating the notification.", errors);
            }
        }
    }
}
