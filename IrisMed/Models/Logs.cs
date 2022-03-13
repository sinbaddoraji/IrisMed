using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class Logs
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string Timestamp { get; set; }
    }
}
