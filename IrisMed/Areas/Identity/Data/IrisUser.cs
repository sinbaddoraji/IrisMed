using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IrisMed.Areas.Identity.Data
{


    public class IrisUser : IdentityUser
    {
        //This will act as staff id and patient id
        //General Details
        [Required]
        public string? FullName { get; set; }

        //Patient details
        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string? DateOfBirth { get; set; }

        [Required]
        [Range(10, int.MaxValue, ErrorMessage = "Please enter a value from 10 - 200")]
        public int Height { get; set; }

        [Required]
        [Range(10, int.MaxValue, ErrorMessage = "Please enter a value from 10 - 200")]
        public int Weight { get; set; }

        public string? AssignedMedication  { get; set; }

        public string? MedicalConditons { get; set; }

        //Staff details

        //Staff type 0 = Patient, 1 = Doctor, 2 = Manager/Receptionsit
        public int StaffType { get; set; }

    }
}
