using FasterCrmApp.Entities.Abstract;

namespace FasterCrmApp.Entities.Concrete
{
    public class Issue : EntityBase
    {
        public string Summary { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
