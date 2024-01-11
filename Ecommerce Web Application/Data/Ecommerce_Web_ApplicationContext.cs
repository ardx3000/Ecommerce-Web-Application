using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Web_Application.Models;

namespace Ecommerce_Web_Application.Data
{
    public class Ecommerce_Web_ApplicationContext : DbContext
    {
        public Ecommerce_Web_ApplicationContext (DbContextOptions<Ecommerce_Web_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Ecommerce_Web_Application.Models.User> User { get; set; } = default!;
    }
}
