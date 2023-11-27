using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models;
using Cake.Models.Authentication;

namespace Cake.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BakeryCakeContext _context;

        public OrdersController(BakeryCakeContext context)
        {
            _context = context;
        }
        public string getRole()
        {
            User u = _context.Users.Where(x => x.UserName.Equals(HttpContext.Session.GetString("UserName"))).SingleOrDefault();
            return u.RoleName;
        }

        [Authentication]
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                var bakeryCakeContext = _context.Orders.Include(o => o.Customer).OrderByDescending(x=> x.OrderId);
                return View(await bakeryCakeContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authentication]
        public async Task<IActionResult> list()
		{
            var userName = HttpContext.Session.GetString("UserName");
            List<Order> orders = _context.Orders
                                         .Include(o => o.Customer)
                                         .Where(o => o.Customer.UserName == userName)
                                         .OrderByDescending(x => x.OrderId)
										 .ToList();
            ViewData["order"] = orders.ToList();
			return View();
		}
        [Authentication]
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            ViewData["orderItems"]= _context.OrderItems.Include(x=>x.Product).Where(x => x.OrderId==order.OrderId).ToList();
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [Authentication]
        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,IsApprove")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "UserId", "UserId", order.CustomerId);
            return View(order);
        }
        [Authentication]
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "UserId", "UserId", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Edit(int id, String status)
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                Order o = _context.Orders.FirstOrDefault(x => x.OrderId == id);
                o.Status = status;
                _context.Update(o);
                _context.SaveChanges();

                var bakeryCakeContext = _context.Orders.Include(o => o.Customer);
                return View("Index", await bakeryCakeContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .Include(o => o.Customer)
        //        .FirstOrDefaultAsync(m => m.OrderId == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        // POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'BakeryCakeContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            List<OrderItem> items = _context.OrderItems.Where(x => x.OrderId == order.OrderId).ToList();
            if (order != null)
            {
                foreach (var item in items)
                {
                    _context.OrderItems.Remove(item);
                }
                
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(list));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
