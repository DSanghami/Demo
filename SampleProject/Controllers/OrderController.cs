using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController

        private readonly CustomerDbContext _context;

        public OrderController(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.Orders != null ?
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'DbCoCustomerntext.BookClasses'  is null.");
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                return View(new Order());
            }
            else // to edit the existing data
            {
                var existingOrder = _context.Orders.Find(id);
                if (existingOrder == null)
                {
                    return NotFound();
                }
                return View(existingOrder);
            }

        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
