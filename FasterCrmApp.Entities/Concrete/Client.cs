using FasterCrmApp.Entities.Abstract;

namespace FasterCrmApp.Entities.Concrete
{
    public class Client : EntityBase
    {
        public string Name { get; set; } = string.Empty; // John Doe or Arçelik
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
        public bool IsCorporate { get; set; }
    }
}
