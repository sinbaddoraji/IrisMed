using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
