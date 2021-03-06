﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VipCon.Data;
using VipCon.Models;

namespace VipCon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, MyIdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Prospect> Prospect { get; set; }

        public DbSet<VipCon.Models.Noticia> Noticia { get; set; }

        public DbSet<VipCon.Models.Parceiro> Parceiro { get; set; }

        // public DbSet<Imagem> Schedule { get; set; }
    }
}
