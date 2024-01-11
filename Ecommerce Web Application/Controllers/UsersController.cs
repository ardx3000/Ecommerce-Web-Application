using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Web_Application.Data;
using Ecommerce_Web_Application.Models;

namespace Ecommerce_Web_Application.Controllers
{
    public class UsersController : Controller
    {
        private readonly Ecommerce_Web_ApplicationContext _context;

        public UsersController(Ecommerce_Web_ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user) 
        {
            //Storing user iformation in plain text in DB only for test.
            //TODO add security for user's credentials.
            //TODO check if the user its already in the database .
            _context.User.Add(user);
            _context.SaveChanges();
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            //A really simple and "silly" way to login, this method does not contain any security. TO NOT BE USED IN AN WEB-APPILCATION THAT INTERACTS WITH USERS.
            //I am using this for a DEMO
            //TODO add security for login.


            var storedUser = _context.User.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            
            if (storedUser != null)
            {
                //Succesful login.
                return RedirectToAction("Welcome");
            }
            else
            {
                //Failed login.
                return RedirectToAction("Login");
            }

        }
      
    }
}
