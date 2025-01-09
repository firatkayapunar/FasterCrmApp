using System.ComponentModel.DataAnnotations;

namespace FasterCrmApp.Entities.Abstract
{
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
