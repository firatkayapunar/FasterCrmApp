namespace FasterCrmApp.Models
{
    public class CreateNotificationModel
    {
        public string Text { get; set; } = string.Empty;
        public int NotifcationType { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    public class EditNotificationModel
    {
        public int ID { get; set; }
        public bool IsRead { get; set; }
    }

    public class DeleteNotificationModel
    {
        public int ID { get; set; }
    }

    public class NotificationModel
    {
        public int ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public int NotificationType { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
