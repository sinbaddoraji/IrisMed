using System.ComponentModel.DataAnnotations;

namespace IrisMed.Data
{
    public class PatientQueries
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }
    }
}
