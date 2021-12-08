using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SAFIB.Models;

namespace SAFIB
{

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Subvision> Subvisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Subvision>().Property(p => p.SubjectionID).Ha;
        }
    }
}
