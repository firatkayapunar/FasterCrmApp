using FasterCrmApp.Entities.Abstract;
using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Entities.Concrete
{
    public class Log : EntityBase
    {
        public string Text { get; set; } = string.Empty;
        public LogType Type { get; set; }

        public int? UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
