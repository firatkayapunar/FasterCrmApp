namespace FasterCrmApp.Models
{
    public class AuthenticateModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateUserModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RePassword { get; set; } = string.Empty;
        public int Role { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int Role { get; set; }
        public bool Locked { get; set; }
    }
}
