using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class StaffBoard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StaffName { get; set; } = "";

        [Required]
        [DataType(DataType.MultilineText)]
        public string StaffMessage { get; set; } = "";
    }
}
