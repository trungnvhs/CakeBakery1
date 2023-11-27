using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models.Authentication;
using Cake.Models;

namespace Cake.Controllers
{
    public class CommentController : Controller
    {
        private readonly BakeryCakeContext _context;
        public CommentController(BakeryCakeContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Add(int BlogID, int? parentID, string message)
        {
            User u = _context.Users.Where(x => x.UserName.Equals(HttpContext.Session.GetString("UserName"))).SingleOrDefault();
            Comment c = new()
            {
                BlogId = BlogID,
                ParentId = parentID,
                Description = message,
                CreateDate = DateTime.Now,
                Owner = u.UserId
            };
			_context.Comments.Add(c);
            _context.SaveChanges();

			
			return RedirectToAction("Details", "Blogs", new {id = BlogID});
        }
    }
}
