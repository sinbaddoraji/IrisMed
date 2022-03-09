using IrisMed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IrisMed.Views.Home
{
    public class ContactUsContext : DbContext
    {
        public DbSet<PatientQueries> Queries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=EFSaving.Basics;Trusted_Connection=True");
        }
    }
    public class ContactUsModel : PageModel
    {
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [DataType(DataType.MultilineText)]
            [Required]
            public string Content { get; set; }
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            PatientQueries patientQueries = new PatientQueries()
            {
                Name = Input.Name,
                Email = Input.Email,
                Content = Input.Content
            };


            using (var contactUsContext = new ContactUsContext())
            {
                contactUsContext.Queries.Add(patientQueries);
                contactUsContext.SaveChanges();
            }
            return RedirectToAction("~/");
        }
    }
}
