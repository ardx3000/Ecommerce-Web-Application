using Microsoft.EntityFrameworkCore;
using Ecommerce_Web_Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce_Web_Application.Data
{
    public class Ecommerce_Web_ApplicationContext : IdentityDbContext<UserViewModel>
    {
        public Ecommerce_Web_ApplicationContext (DbContextOptions<Ecommerce_Web_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Ecommerce_Web_Application.Models.UserViewModel> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Attributes that are ignored. If you want to add anything in the database or in the application make sure to delete it from the ignore list.
            modelBuilder.Entity<UserViewModel>()
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.Email)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.SecurityStamp)
                .Ignore(c => c.NormalizedEmail);


        }
    }
}
