using Ecommerce_Web_Application.Data;
using Ecommerce_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Ecommerce_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Ecommerce_Web_ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, Ecommerce_Web_ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddJob(string jobTitle, string jobDescription)
        {
            //Get Logged user.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var jobPost = new JobPostViewModel
            {
                Title = jobTitle,
                Description = jobDescription,
                UserId = userId,
                CreatedDate = DateTime.Now
            };

            _context.JobPost.Add(jobPost);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}