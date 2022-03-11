using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StaffName { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        public string Shift_Date { get; set; } = "";

        [Required]
        [DataType(DataType.Time)]
        public string Shift_Start { get; set; } = "";

        [Required]
        [DataType(DataType.Time)]
        public string Shift_End { get; set; } = "";
    }
}
