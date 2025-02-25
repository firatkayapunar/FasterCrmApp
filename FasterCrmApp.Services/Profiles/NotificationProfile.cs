using AutoMapper;
using FasterCrmApp.Entities;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<CreateNotificationModel, Notification>().ReverseMap();
            CreateMap<EditNotificationModel, Notification>().ReverseMap();
            CreateMap<DeleteNotificationModel, Notification>().ReverseMap();
            CreateMap<NotificationModel, Notification>().ReverseMap();
        }
    }
}
