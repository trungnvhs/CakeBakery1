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
    public class BlogsController : Controller
    {
        private readonly BakeryCakeContext _context;

        public BlogsController(BakeryCakeContext context)
        {
            _context = context;
        }
        public string getRole()
        {
            User u = _context.Users.Where(x => x.UserName.Equals(HttpContext.Session.GetString("UserName"))).SingleOrDefault();
            return u.RoleName;
        }
        [Authentication]
        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            //var bakeryCakeContext = _context.Blogs.Include(b => b.CreateByNavigation);
            //List<Blog> BlogsList = await _context.Blogs.ToListAsync();
            List<Blog> BlogsList = _context.Blogs.Include(b => b.CreateByNavigation).ToList();

            var totalComment = _context.Comments
                .GroupBy(b => b.BlogId)
                .Select(g => new { BlogID = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.TotalComment = totalComment;
            ViewBag.BlogsList = BlogsList;

            return View();
        }
        [Authentication]
        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.CreateByNavigation)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(x=>x.OwnerNavigation)
                .Where(c => c.BlogId == id && c.ParentId == null).ToListAsync();

            var listBlog = _context.Blogs
				.Include(b => b.CreateByNavigation)
				.Where(x=>x.BlogId!=id)
                .OrderByDescending(x=>x.CreateDate)
                .Take(4).ToList();

            ViewData["listBlog"] = listBlog;
			ViewData["count"] = _context.Comments
				.Include(x => x.OwnerNavigation)
				.Where(c => c.BlogId == id).ToList().Count;
            ViewData["cmt"] = comments;
            ViewData["blog"] = blog;

			return View();
        }
        [Authentication]
        // GET: Blogs/Create
        public IActionResult Create()
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "UserId");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
                
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Create([Bind("BlogId,BlogTitle,Description,Image")] Blog blog)
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                if (ModelState.IsValid)
                {
                    blog.CreateDate = DateTime.Now;
                    blog.CreateBy = _context.Users.Where(x => x.UserName.Equals(HttpContext.Session.GetString("UserName"))).SingleOrDefault().UserId;
                    _context.Add(blog);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "UserId", blog.CreateBy);
                return View(blog);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        [Authentication]
        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                if (id == null || _context.Blogs == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return NotFound();
                }
                ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "UserId", blog.CreateBy);
                return View(blog);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,BlogTitle,Description,CreateBy,CreateDate")] Blog blog)
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                if (id != blog.BlogId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(blog);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BlogExists(blog.BlogId))
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
                ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "UserId", blog.CreateBy);
                return View(blog);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [Authentication]
        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                if (id == null || _context.Blogs == null)
                {
                    return NotFound();
                }

                var blog = await _context.Blogs
                    .Include(b => b.CreateByNavigation)
                    .FirstOrDefaultAsync(m => m.BlogId == id);
                if (blog == null)
                {
                    return NotFound();
                }

                return View(blog);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                if (_context.Blogs == null)
                {
                    return Problem("Entity set 'BakeryCakeContext.Blogs'  is null.");
                }
                var blog = await _context.Blogs.FindAsync(id);
                if (blog != null)
                {
                    _context.Blogs.Remove(blog);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
        [Authentication]
        public async Task<IActionResult> List()
        {
            if (getRole().Equals("Admin") || getRole().Equals("Staff"))
            {
                //var bakeryCakeContext = _context.Blogs.Include(b => b.CreateByNavigation);
                //List<Blog> BlogsList = await _context.Blogs.ToListAsync();
                List<Blog> BlogsList = _context.Blogs.Include(b => b.CreateByNavigation).ToList();

                var totalComment = _context.Comments
                    .GroupBy(b => b.BlogId)
                    .Select(g => new { BlogID = g.Key, Count = g.Count() })
                    .ToList();

                ViewBag.TotalComment = totalComment;
                ViewBag.BlogsList = BlogsList;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}