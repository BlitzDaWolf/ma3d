using Ma3d.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        // public DbSet<Role> Roles { get; set; }
        // public DbSet<UserRole> UserRole { get; set; }
    }
}
