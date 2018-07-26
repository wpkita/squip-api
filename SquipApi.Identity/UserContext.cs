using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SquipApi.Pocos;

namespace SquipApi.Identity
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("User");
                b.HasKey(u => u.Id);
                b.HasAlternateKey(u => u.ThirdPartyId);
            });
        }
    }
}
