using Domain.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<User> DomainUsers { get; set; }
        public DbSet<ItServiceProvider> ItServiceProviders { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne<ApplicationUser>() 
                .WithOne()
                .HasForeignKey<User>(u => u.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ItServiceProvider>()
                .HasOne(p => p.DomainUser)
                .WithMany()
                .HasForeignKey(p => p.DomainUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Company>()
                .HasOne(p => p.DomainUser)
                .WithMany()
                .HasForeignKey(p => p.DomainUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
