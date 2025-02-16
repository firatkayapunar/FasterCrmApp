using FasterCrmApp.Entities.Abstract;
using FasterCrmApp.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace FasterCrmApp.Entities.Concrete
{
    public class Log : EntityBase
    {
        [Required, StringLength(160)]
        public string Text { get; set; } = string.Empty;

        public LogType Type { get; set; }


        public int? UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
