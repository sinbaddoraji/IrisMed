using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrisMed.Models
{
    public class Career
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [NotMapped]
        public IFormFile Resume { get; set; }

        [Required]
        [NotMapped]
        public IFormFile CoverLetter { get; set; }
    }
}
