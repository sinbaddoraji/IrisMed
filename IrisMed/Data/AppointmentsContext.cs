#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IrisMed.Models;

namespace IrisMed.Data
{
    public class AppointmentsContext : DbContext
    {
        public AppointmentsContext (DbContextOptions<AppointmentsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        public DbSet<IrisMed.Models.Appointment> Appointments { get; set; }
    }
}
