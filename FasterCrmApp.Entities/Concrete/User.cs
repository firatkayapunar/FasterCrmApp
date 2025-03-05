using FasterCrmApp.Entities.Abstract;
using FasterCrmApp.Entities.Enum;

namespace FasterCrmApp.Entities.Concrete
{
    public class User : EntityBase
    {
        private readonly ICollection<Issue> _issues;

        public User()
        {
            _issues = new List<Issue>();
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsLocked { get; set; }

        public ICollection<Issue> Issues
        {
            get
            {
                return _issues;
            }
        }
    }
}
