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
        public DbSet<Ecommerce_Web_Application.Models.JobPostViewModel> JobPost { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Attributes that are ignored. If you want to add anything in the database or in the application make sure to delete it from the ignore list.
            modelBuilder.Entity<UserViewModel>()
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.SecurityStamp);

            modelBuilder.Entity<JobPostViewModel>()
                .HasOne(j => j.User)
                .WithMany(u => u.JobAdverts)
                .HasForeignKey(u => u.UserId);



        }
    }
}
