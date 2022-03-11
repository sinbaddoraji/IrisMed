using System.ComponentModel.DataAnnotations;

namespace IrisMed.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientComplaints { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string AppointmentTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string AppointmentDate { get; set; }
    }
}
