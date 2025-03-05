using FasterCrmApp.Entities.Abstract;
using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Entities.Concrete
{
    public class Lead : EntityBase
    {
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public LeadType LeadType { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public int? UserID { get; set; }
        public virtual User? User { get; set; }
        public int? ClientID { get; set; }
        public virtual Client? Client { get; set; }
    }
}
