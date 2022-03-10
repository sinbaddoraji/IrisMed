#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IrisMed.Models;

namespace IrisMed.Data
{
    public class CareersContext : DbContext
    {
        public CareersContext (DbContextOptions<CareersContext> options)
            : base(options)
        {
        }

        public DbSet<IrisMed.Models.Career> CareersModel { get; set; }
    }
}
