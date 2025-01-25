using FasterCrmApp.Entities.Abstract;
using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Entities.Concrete
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool Locked { get; set; }
    }
}
