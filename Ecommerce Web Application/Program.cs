using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce_Web_Application.Data;
using Ecommerce_Web_Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Ecommerce_Web_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Ecommerce_Web_ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Ecommerce_Web_ApplicationContext") ?? throw new InvalidOperationException("Connection string 'Ecommerce_Web_ApplicationContext' not found.")));


            // ASP.NET Core identity service.
            builder.Services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<Ecommerce_Web_ApplicationContext>()
                .AddDefaultTokenProviders();
             

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "register",
                pattern: "{controller=UserModels}/{action=Register}/{id?}");

            app.MapControllerRoute(
                name: "login",
                pattern: "{controller=UserModels}/{action=Login}/{id?}");

            app.Run();
        }
    }
}