using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace SampleProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerDbContext _context;
      
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Something> somethings = new List<Something>();
            somethings.Add(new Something()
            {
                Id = 1,
                Title = "Some 1",
                Img = "1.jpg"
            });
            somethings.Add(new Something()
            {
                Id = 2,
                Title = "Some 2",
                Img = "2.jpg"
            });
            somethings.Add(new Something()
            {
                Id = 3,
                Title = "Some 3", 
                Img = "3.jpg"
            });

            return View(somethings);
        }
        
        //public List<object> GetAttendace()
        //{
        //    List<object> data = new List<object>();
        //    List<string> labels = _context.Pie.Select(a => a.WorkingDays).ToList();

        //}
       
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login","Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}