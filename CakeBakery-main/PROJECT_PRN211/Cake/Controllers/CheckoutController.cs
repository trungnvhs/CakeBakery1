using Cake.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cake.Controllers
{
    
    public class CheckoutController : Controller
    {
        BakeryCakeContext _context = new BakeryCakeContext();
        [HttpPost]
        public IActionResult Index(decimal total)
        {
            if(total > 0) 
            {
                string user_name = HttpContext.Session.GetString("UserName");
                User u = _context.Users.FirstOrDefault(x => x.UserName == user_name);
                ViewData["count"] = total;
				ViewData["cus"] = u;
				return View();
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
            
        }

    }
}
