using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class ProductController : Controller
    {

        private readonly CustomerDbContext _context;

        public ProductController(CustomerDbContext context)
        {
            _context = context;
        }

        // GET: ProductController

        public async Task<IActionResult> Index()
        {
            return _context.Products != null ?
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'DbCustomertext.Product'  is null.");
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customers/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                return View(new Product());
            }
            else // to edit the existing data
            {
                var existingProduct = _context.Products.Find(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                return View(existingProduct);
            }

        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StockId,ProductName,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _context.Add(product);
                }


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
