using FasterCrmApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasterCrmApp.Entities.Concrete
{
    [Table("Clients")]
    public class Client : BaseEntity
    {

        [Required, StringLength(60)]
        public string Name { get; set; } = string.Empty; // John Doe or Arçelik

        [Required, StringLength(60)]
        public string Email { get; set; } = string.Empty;

        [StringLength(25)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
