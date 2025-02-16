using System.ComponentModel.DataAnnotations;

namespace FasterCrmApp.Entities.Abstract
{
    public abstract class EntityBase
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
