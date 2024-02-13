using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Ecommerce_Web_Application.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;


//TODO fix the bug where the UI is updating regardless of the correct password(It does not execute a SQL querry)

namespace Ecommerce_Web_Application.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<UserViewModel> _userManager;
        private readonly SignInManager<UserViewModel> _signInManager;

        public UsersController(UserManager<UserViewModel> userManager, SignInManager<UserViewModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel user){
        
            if (ModelState.IsValid)
            {
                var newUser = new UserViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,

                };

                if (!string.Equals(user.PasswordHash , user.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfrimPassword", "The password and confirmation password do not match.");
                    return View();
                }

                var result = await _userManager.CreateAsync(newUser, user.PasswordHash);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserProfile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("UserProfile");
            }

            ModelState.AddModelError("", "Invalid login attempt! ");
            return View();
        }

        public async Task<IActionResult> UserProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var userViewModel = new UserViewModel
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName,
                    };

                    var userEditViewModel = new UserEditViewModel
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                    };

                    // Pass both UserViewModel and UserEditViewModel to the view using ViewData
                    ViewData["UserViewModel"] = userViewModel;
                    return View(userEditViewModel);
                }
            }

            // Handle the case where user is not authenticated or information retrieval failed
            return RedirectToAction("Login");
        }
 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile(UserEditViewModel updateUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }


                var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, updateUser.PasswordValidation);

                if (!isOldPasswordValid)
                {
                    ModelState.AddModelError("PasswordValidation", "Password does not match the old password");
                    return View(updateUser);
                }

                user.Email = updateUser.Email;
                user.PhoneNumber = updateUser.PhoneNumber;

                if (!string.IsNullOrEmpty(updateUser.NewPassword))
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateUser.NewPassword);
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Optionally sign in again if user information is updated
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("UserProfile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If ModelState is not valid, return to the view with validation errors
            return View(updateUser);
        }

        private bool IsValidEmailFormat(string email)  
        {
            var emailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
            return emailRegex.IsMatch(email);
        }

        public IActionResult ProfileUpdated()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
