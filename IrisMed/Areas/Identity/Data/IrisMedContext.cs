﻿using IrisMed.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IrisMed.Data;

public class IrisMedContext : IdentityDbContext<IrisUser>
{
    public IrisMedContext(DbContextOptions<IrisMedContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<IrisUser>(b =>
        {
            b.HasKey("Id");
        });

    }
}
