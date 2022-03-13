#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IrisMed.Models;

namespace IrisMed.Data
{
    public class LogsContext : DbContext
    {
        public LogsContext (DbContextOptions<LogsContext> options)
            : base(options)
        {
        }

        public DbSet<IrisMed.Models.Logs> Logs { get; set; }
    }
}
