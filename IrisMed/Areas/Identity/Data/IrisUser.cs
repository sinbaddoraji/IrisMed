using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IrisMed.Areas.Identity.Data
{
    public class IrisUser : IdentityUser
    {
        //This will act as staff id and patient id

        //General Details

        public string FullName { get; set; }

        public int? Assigned_ward { get; set; }

        //Patient details
        public string? Gender { get; set; }

        public string? DateOfBirth { get; set; }

        public string? Ethniticy { get; set; }

        public string? Height { get; set; }

        public string? Weight { get; set; }

        public string? AssignedMedication  { get; set; }

        public string? MedicalConditons { get; set; }

        //Staff details

        //Staff type 0 = Patient, 1 = Doctor, 2 = Manager/Receptionsit
        public int? StaffType { get; set; }

    }
}
