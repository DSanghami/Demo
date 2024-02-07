using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SampleProject.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly CustomerDbContext _dbContext;

        public AccessController(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin )
        {  
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == modelLogin.Email && u.Password == modelLogin.Password);

            if(user!= null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim("OtherProperties","Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
              AllowRefresh = true,
              IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home" );
            }
            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        // GET: Customers/Create
        public IActionResult Register(int id = 0)
        {
            if (id == 0)
            {
                return View(new User());
            }
            else // to edit the existing data
            {
                var existingUser = _dbContext.Users.Find(id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                return View(existingUser);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind( "Id,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    _dbContext.Add(user);
                }


                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }
    }
}
