#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IrisMed.Models;

namespace IrisMed
{
    public class DataShiftContext : DbContext
    {
        public DataShiftContext (DbContextOptions<DataShiftContext> options)
            : base(options)
        {
        }

        public DbSet<IrisMed.Models.Shift> Shift { get; set; }
    }
}
