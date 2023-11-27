using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models.Authentication;
using Cake.Models;

namespace Cake.Controllers
{
    public class UsersController : Controller
    {
        private readonly BakeryCakeContext _context;

        public UsersController(BakeryCakeContext context)
        {
            _context = context;
        }
        public string getRole()
        {
            User u = _context.Users.Where(x=> x.UserName.Equals(HttpContext.Session.GetString("UserName"))).SingleOrDefault();
            return u.RoleName;
        }
        [Authentication]
        //[Authentication]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (getRole().Equals("Admin"))
            {
                return _context.Users != null ?
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'BakeryCakeContext.Users'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authentication]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id, string? mod)
        {
            if (mod != null)
            {
                ViewData["view"] = "view";
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [Authentication]
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,Name,Phone,Mail,Sex,Dob,Avatar,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [Authentication]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,Name,Phone,Mail,Sex,Dob,Avatar,IsActive")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [Authentication]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'BakeryCakeContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
