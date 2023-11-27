using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Cake.Infrastructure;
using Cake.Models.Authentication;
using Cake.Models;

namespace Cake.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BakeryCakeContext _context;

        public ProductsController(BakeryCakeContext context)
        {
            _context = context;
        }
        public string getRole()
        {
            User u = _context.Users.Where(x => x.UserName.Equals(HttpContext.Session.GetString("UserName"))).SingleOrDefault();
            return u.RoleName;
        }
        [Authentication]
        // GET: Products
        public async Task<IActionResult> Index(string sortOrder,
                                               string currentFilter,
                                               string searchString,
                                               int? pageNumber,
                                               int? categoryId,
                                               decimal? minPrice,
                                               decimal? maxPrice)
        {
            var categoryList = await _context.Categories.ToListAsync();
            ViewData["CategoryList"] = categoryList;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["UnitPriceSortParm"] = sortOrder == "UnitPrice" ? "unitprice_desc" : "UnitPrice";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            var products = from p in _context.Products.Include(p => p.Category)
                           select p;
            int totalCount = await products.CountAsync();
            ViewData["TotalCount"] = totalCount;

            // Lọc theo tên sản phẩm
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            // Lọc theo giá sản phẩm
            if (minPrice != null && maxPrice != null)
            {
                products = products.Where(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice);
            }

            // Lọc theo CategoryID 
            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
                ViewData["CurrentCategory"] = categoryId;
            }
            switch (sortOrder)
            {
                case "id_desc":
                    products = products.OrderByDescending(p => p.ProductId);
                    break;
                case "Name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "UnitPrice":
                    products = products.OrderBy(p => p.UnitPrice);
                    break;
                case "unitprice_desc":
                    products = products.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductId);
                    break;
            }

            int pageSize = 9;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [Authentication]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            var productsWithSameCategory = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryId == product.Category.CategoryId && p.ProductId != id)
                .ToListAsync();

            ViewData["ProductsWithSameCategory"] = productsWithSameCategory;

            return View(product);
        }
        [Authentication]
        // GET: Products/Create
        public IActionResult Create()
        {
            if (getRole().Equals("Admin"))
            {
                ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Create([Bind("ProductId,Name,UnitPrice,UnitInStock,Image,CategoryId,Discontinued,Discount,Describe")] Product product)
        {
            if (getRole().Equals("Admin"))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [Authentication]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (getRole().Equals("Admin"))
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,UnitPrice,UnitInStock,Image,CategoryId,Discontinued,Discount,Describe")] Product product)
        {
            if (getRole().Equals("Admin"))
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", new { id = id });
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
                
        }
        [Authentication]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (getRole().Equals("Admin"))
            {
                var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
                ViewData["Category"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authentication]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (getRole().Equals("Admin"))
            {
                if (_context.Products == null)
                {
                    return Problem("Entity set 'BakeryCakeContext.Products'  is null.");
                }
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        [Authentication]
        public async Task<IActionResult> List()
        {
            if (getRole().Equals("Admin"))
            {
                return _context.Products != null ?
                        View(await _context.Products.Include(p => p.Category).ToListAsync()) :
                        Problem("Entity set 'BakeryCakeContext.Products'  is null.");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
