namespace FasterCrmApp.Models
{
    public class CreateLogModel
    {
        public string Text { get; set; } = string.Empty;
        public int LogType { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class EditLogModel
    {
        public int ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public int LogType { get; set; }
    }

    public class DeleteLogModel
    {
        public int ID { get; set; }
    }

    public class LogModel
    {
        public int ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public int LogType { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
