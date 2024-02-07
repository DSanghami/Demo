using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerDbContext _context;

        public CustomersController(CustomerDbContext context)
        {
            _context = context;
        }

        //GET: Customers
        public async Task<IActionResult> GetOrders()
        {
            return _context.Orders != null ?
                        View(await _context.Orders.ToListAsync()) :
                        Problem("Entity set 'CustomerDbContext.Customers'  is null.");
        }

        public async Task<IActionResult> Index(string searchName, string searchAddress, string selectedAddress)
        {
            IQueryable<Customer> customers = _context.Customers;

            // Filtering logic based on the search criteria
            if (!string.IsNullOrEmpty(searchName))
            {
                customers = customers.Where(c => c.Name.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchAddress))
            {
                customers = customers.Where(c => c.Address.Contains(searchAddress));
            }

            // Additional filtering based on selectedAddress
            if (!string.IsNullOrEmpty(selectedAddress))
            {
                selectedAddress = selectedAddress.ToLower(); // or use .ToUpper() for case-insensitive comparison
                customers = customers.Where(c => c.Address.ToLower().Equals(selectedAddress));
            }

            // Prepare data for the dropdown filter
            ViewBag.Addresses = GetUniqueAddresses();

            
            return View(await customers.ToListAsync());
        }

        private IEnumerable<string> GetUniqueAddresses()
        {
            // Retrieve distinct addresses from the database
            return _context.Customers.Select(c => c.Address).Distinct().ToList();
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                return View(new Customer());
            }
            else // to edit the existing data
            {
                var existingCustomer = _context.Customers.Find(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }
                return View(existingCustomer);
            }

        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Id == 0)
                {
                    _context.Add(customer);
                }
              

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            return View(existingCustomer);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                var existingCustomer = _context.Customers.Find(customer.Id);
                if (existingCustomer == null)
                {
                    return NotFound(); 
                }

                
                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

       

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'CustomerDbContext.CustomerClasses'  is null.");
            }
            var customerClass = await _context.Customers.FindAsync(id);
            if (customerClass != null)
            {
                _context.Customers.Remove(customerClass);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        
        public List<object> GetAttendace()
        {
            List<object> data = new List<object>();
            List<string> labels = _context.Attendaces.Select(m => m.MonthName).ToList();
            List<int> total = _context.Attendaces.Select(t => t.Total).ToList();
            data.Add(labels);
            data.Add(total);
            return data;


        }

        public async Task<IActionResult> Register([Bind("Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    _context.Add(user);
                }


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Register));
            }
            return View(user);
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
