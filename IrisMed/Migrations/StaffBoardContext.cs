#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IrisMed.Models;

namespace IrisMed.Data
{
    public class StaffBoardContext : DbContext
    {
        public StaffBoardContext (DbContextOptions<StaffBoardContext> options)
            : base(options)
        {
        }

        
    }
}
