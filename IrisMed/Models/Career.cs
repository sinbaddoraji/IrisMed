using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class Career
    {
        public int Id { get; set; }

        public string FirstName { get; set; }    

        public string LastName { get; set; }

        [Key]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public byte[] Resume { get; set; }

        public byte[] CoverLetter { get; set; }
    }
}
