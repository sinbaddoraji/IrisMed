using IrisMed.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using IrisMed.Models;

namespace IrisMed.Models
{
    public class ContactUsContext : DbContext
    {
        public ContactUsContext(DbContextOptions<ContactUsContext> options) : base(options)
        {
        }
        public DbSet<PatientQueries> Queries { get; set; }

    }

    public class ContactUsModel : PageModel
    {
        [Key]
        [Required]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; } = "";
    }
}
