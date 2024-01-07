using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Web_Application.Data;
using Ecommerce_Web_Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Web_Application.Controllers
{
    public class UserModelsController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public UserModelsController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if UserNamel or email is already registerd in DB.
                if (await _userManager.FindByNameAsync(model.Email) != null /*|| await _userManager.FindByEmailAsync(model.Email) != null add this for a seccound condition like username and email should not be in db */)
                {
                    ModelState.AddModelError("", "Username or email is already used! ");
                    return View(model);
                }

                // Create  and store a user with hashed password.
                var user = new UserModel { Email = model.Email };

                var result = await _userManager.AddPasswordAsync(user, model.Password);

                if (result.Succeeded)
                {

                    // Auto sign in the user after registration.
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect to a home page after a succsesful registration.

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If the model is invalid return to registration page with validation errors
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                    // If succses redirect to home page
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    // Lock user if multiple attempts to log in
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Account locked out! (To many login attempts)");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName or password!");
                    }
                }
                else 
                {
                    ModelState.AddModelError("", "Invalid UserName or password!");
                }
            }
            return View(model);
        }
    }
}
