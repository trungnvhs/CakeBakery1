using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Cake.Models;

namespace Cake.Controllers
{
    public class AccessController : Controller
    {
        BakeryCakeContext _context = new BakeryCakeContext();
        [HttpGet]
        public IActionResult Login()
        {
			if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(User user) {
			if (HttpContext.Session.GetString("UserName") == null)
            {
                User? u = _context.Users?.Where(x =>
                (x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password))
                || (x.Mail.Equals(user.UserName) && x.Password.Equals(user.Password))
                || (x.Phone.Equals(user.UserName) && x.Password.Equals(user.Password))).SingleOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mess"] = "Username or Password incorrect";
                    return View();
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
		public IActionResult Register(User user)
		{
            bool check = false;
            User u = _context.Users.Where(x => x.UserName.Equals(user.UserName)).SingleOrDefault();
            if(u != null)
            {
                ViewData["duplicate"] = "UserName has existed.";
                check = true;
            }
            if (user.Password.Length < 8 || user.Password.Length >20)
            {
                ViewData["PassLength"] = "Password must be from 8 to 20 characters";
				check = true;
			}
            if (!user.Password.Equals(user.Name))
            {
				ViewData["rePass"] = "Comfirm Password incorrect.";
				check = true;
			}
            if(check)
            {
				return View();
            }
            else
            {
                user.Name = null;
                user.Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU";
                user.RoleName = "Customer";
                user.IsActive = true;
                _context.Users.Add(user);
                _context.SaveChanges();
				return RedirectToAction("Login", "Access");
			}
		}
        [HttpGet]
        public IActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePass(User user)
        {
            bool check = false;
            User u = _context.Users.Where(x => x.UserName.Equals(user.UserName)).SingleOrDefault();
            if (u != null)
            {
                ViewData["duplicate"] = "UserName has existed.";
                check = true;
            }
            if (user.Password.Length < 8 || user.Password.Length > 20)
            {
                ViewData["PassLength"] = "Password must be from 8 to 20 characters";
                check = true;
            }
            if (!user.Password.Equals(user.Name))
            {
                ViewData["rePass"] = "Comfirm Password incorrect.";
                check = true;
            }
            if (check)
            {
                return View();
            }
            else
            {
                user.Name = null;
                user.Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7vWHjatimRMCENuZGo1-EAb5-Vs8QWHuVgg&usqp=CAU";
                user.RoleName = "Customer";
                user.IsActive = true;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login", "Access");
            }
        }
    }
}
