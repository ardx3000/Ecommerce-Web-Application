using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Web_Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce_Web_Application.Data
{
    public class Ecommerce_Web_ApplicationContext : IdentityDbContext<UserModel>
    {
        public Ecommerce_Web_ApplicationContext (DbContextOptions<Ecommerce_Web_ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
