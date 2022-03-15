using IrisMed.Areas.Identity.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IrisMed.Data
{
    public class ApplicationDbContext : IdentityDbContext<IrisUser>
    {
        public override DbSet<IrisUser> Users { get; set; }
        public DbSet<StaffBoard> StaffBoard { get; set; }
        public DbSet<IrisMed.Models.Appointment> Appointments { get; set; }
        public DbSet<IrisMed.Models.Shift> Shift { get; set; }

        public DbSet<IrisMed.Models.Inventory> Inventory { get; set; }

        public DbSet<IrisMed.Models.ContactUs> Queries { get; set; }

        public DbSet<IrisMed.Models.Logs> Logs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<IrisUser>()
            .ToTable("IrisUser");

            modelBuilder.Entity<ContactUs>()
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