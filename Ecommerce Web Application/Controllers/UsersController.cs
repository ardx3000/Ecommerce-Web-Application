using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Ecommerce_Web_Application.Models;

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
        //TODO add email address for registration and phone number.
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel user){
        
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded)
                {
                    return RedirectToAction("Welcome");
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
        public async Task<IActionResult> Login(UserViewModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Welcome");
            }

            ModelState.AddModelError("", "Invalid login attempt! ");
            return View();
        }
      
    }
}
