using Microsoft.AspNetCore.Mvc;
using Cake.Infrastructure;
using Cake.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Cake.Models;

namespace Cake.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly BakeryCakeContext _context;

		public CartController(BakeryCakeContext context)
        {
            _context = context;
        }
        [Authentication]
        public IActionResult Index()
		{ 
			//List<OrderItem> o = _context.OrderItems.Include(x=>x.Order.IsSuccess==false).Include(x=>x.Product).ToList();
            //HttpContext.Session.SetJson("cart", o);
            return View("Cart", HttpContext.Session.GetJson<Cart>("cart") ?? new Cart());
		}
		[Authentication]
		public IActionResult AddToCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, 1);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return View("Cart", Cart);
		}
        [Authentication]
        public IActionResult RemoveToCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, -1);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return View("Cart", Cart);
		}
        [Authentication]
        public IActionResult RemoveFromCart(int productId)
		{
			Product? product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart");
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);

			}
			return View("Cart", Cart);
		}
		[HttpPost]
		[Authentication]
		public IActionResult Order(string phone, string address, string message)
		{
			if (phone==null || address==null)
			{

			}
			int cusid = _context.Users.SingleOrDefault(x => x.UserName.Equals(HttpContext.Session.GetString("UserName"))).UserId;
			Cart = HttpContext.Session.GetJson<Cart>("cart");
			Order order = new()
			{
				CustomerId = cusid,
				OrderDate = DateTime.Now,
				Status = "Processing",
				Address = address,
				Phone = phone,
				Note = message
			};
			_context.Orders.Add(order);
			_context.SaveChanges();
			order = _context.Orders.OrderBy(x=>x.OrderId).LastOrDefault(x => x.CustomerId == cusid);
            foreach (var item in Cart.Lines)
            {
				OrderItem orderItem = new() {
					OrderId = order.OrderId,
					ProductId = item.Product.ProductId,
					Quantity = item.Quantity,
				};
				_context.OrderItems.Add(orderItem);	
				_context.SaveChanges();
            }
			HttpContext.Session.Remove("cart");
			ViewData["OrderSuccess"] = "Order Success";
            ViewData["order"] = _context.Orders.Include(o => o.Customer).Where(o => o.Customer.UserName == HttpContext.Session.GetString("UserName"));
            return RedirectToAction("list", "Orders");
        }
	}
}
