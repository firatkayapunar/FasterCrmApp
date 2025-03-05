namespace FasterCrmApp.Models
{
    public class CreateIssueModel
    {
        public string Summary { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public int UserID { get; set; }
    }

    public class EditIssueModel
    {
        public int ID { get; set; }
        public string Summary { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int UserID { get; set; }
    }

    public class DeleteIssueModel
    {
        public int ID { get; set; }
    }

    public class IssueModel
    {
        public int ID { get; set; }
        public string Summary { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
