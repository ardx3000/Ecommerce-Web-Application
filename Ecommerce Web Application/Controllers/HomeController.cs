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


        //get ...
        [HttpGet]
        public IActionResult Index()
        {
            var jobPosts = _context.JobPost.ToList();
            return View(jobPosts);
        }

        //httpGet data to build the views




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddJob(string jobTitle, string jobDescription)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(jobTitle) && !string.IsNullOrEmpty(jobDescription))
                {
                    //Get Logged user.
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var newJobPost = new JobPostViewModel
                    {
                        Title = jobTitle,
                        Description = jobDescription,
                        CreatedDate = DateTime.Now,
                        UserId = userId
                    };

                    _context.JobPost.Add(newJobPost);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Please provide both job title and description.");
                }
            }
            // If model state is not valid, return to the view with validation errors
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}