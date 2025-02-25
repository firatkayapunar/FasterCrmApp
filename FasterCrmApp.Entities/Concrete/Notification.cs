using FasterCrmApp.Entities.Abstract;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Entities
{
    public class Notification : EntityBase
    {
        public string Text { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
