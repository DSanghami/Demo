using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class OrderListController : Controller
    {
        // GET: OrderListController

        private readonly CustomerDbContext _context;

        public OrderListController(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.OrderLists != null ?
                          View(await _context.OrderLists.ToListAsync()) :
                          Problem("Entity set 'DbCustomertext.Product'  is null.");
        }

        // GET: OrderListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderListController/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                return View(new OrderList());
            }
            else // to edit the existing data
            {
                var existingOrder = _context.OrderLists.Find(id);
                if (existingOrder == null)
                {
                    return NotFound();
                }
                return View(existingOrder);
            }

        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Products,Price,Quantity,Total")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                if (orderList.Id == 0)
                {
                    _context.Add(orderList);
                }


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderList);
        }
        // GET: OrderListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderListController/Edit/5
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

        // GET: OrderListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderListController/Delete/5
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
