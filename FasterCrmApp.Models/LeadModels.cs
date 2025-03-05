namespace FasterCrmApp.Models
{
    public class CreateLeadModel
    {
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? UserID { get; set; }
        public int? ClientID{ get; set; }
    }

    public class EditLeadModel
    {
        public int ID { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int LeadType { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int UserID { get; set; }
        public int ClientID { get; set; }
    }

    public class DeleteLeadModel
    {
        public int ID { get; set; }
    }

    public class LeadModel
    {
        public int ID { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int LeadType { get; set; }
        public string LeadTypeName { get; set; } = string.Empty ;
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int ClientID { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
