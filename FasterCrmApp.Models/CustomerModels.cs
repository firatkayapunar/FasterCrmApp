namespace FasterCrmApp.Models
{
    public class CreateCustomerModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Locked { get; set; }
        public bool IsCorporate { get; set; }
    }

    public class UpdateCustomerModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Locked { get; set; }
        public bool IsCorporate { get; set; }
    }

    public class DeleteCustomerModel
    {
        public int ID { get; set; }
    }

    public class CustomerModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Locked { get; set; }
        public bool IsCorporate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
