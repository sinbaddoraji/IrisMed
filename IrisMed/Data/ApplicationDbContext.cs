using IrisMed.Areas.Identity.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IrisMed.Data
{
    public class ApplicationDbContext : IdentityDbContext<IrisUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<IrisUser>()
            .ToTable("IrisUser");

            modelBuilder.Entity<PatientQueries>()
            .ToTable("Queries").HasKey("Id");

            modelBuilder.Entity<Appointment>()
            .ToTable("Appointments");

            modelBuilder.Entity<Inventory>()
            .ToTable("Inventory");

            modelBuilder.Entity<Logs>()
            .ToTable("Logs");

            modelBuilder.Entity<Shift>()
            .ToTable("Shift");

            modelBuilder.Entity<StaffBoard>()
            .ToTable("StaffBoard");

        }

    }
}