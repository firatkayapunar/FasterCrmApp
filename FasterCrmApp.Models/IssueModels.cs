namespace FasterCrmApp.Models
{
    public class CreateIssueModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
        public bool IsCorporate { get; set; }
    }

    public class EditIssueModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
        public bool IsCorporate { get; set; }
    }

    public class DeleteIssueModel
    {
        public int ID { get; set; }
    }

    public class IssueModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
        public bool IsCorporate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
