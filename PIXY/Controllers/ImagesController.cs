using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;

namespace PIXY.Controllers
{
    public class ImagesController : Controller
    {
        private readonly PIXYContext _context;

        public ImagesController(PIXYContext context)
        {
            _context = context;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login

                var query = from i in _context.Images
                            join u in _context.Users on i.UserId equals u.ID
                            select new ImageVM
                            {
                                ID = i.ID,
                                UserId = i.UserId,
                                CategoryDesc = i.CategoryDesc,
                                ImageType = i.ImageType,
                                FilePathWatermark = i.FilePathWatermark,
                                FilePath = i.FilePath,
                                Price = i.Price,
                                HaveHardcopy = i.HaveHardcopy,
                                IsPurchased = false,
                                AuthorName = u.FirstName + " " + u.LastName
                            };

                ImageVM[] results = query.ToArray();

                return View(results);
            }
            else
            {
                // Have login

                int UserID = (int)HttpContext.Session.GetInt32("UserID");

                var query = from i in _context.Images
                            from u in _context.Users.Where(u => u.ID == i.UserId)
                            from c in _context.Carts.Where(c => c.ImageId == i.ID).DefaultIfEmpty() //Left Join
                            from p in _context.PurchasedItems.Where(p=> i.ID==p.ImageId && p.UserId == UserID ).DefaultIfEmpty() //Left Join
                            select new ImageVM
                            {
                                ID = i.ID,
                                UserId = i.UserId,
                                CategoryDesc = i.CategoryDesc,
                                ImageType = i.ImageType,
                                FilePathWatermark = i.FilePathWatermark,
                                FilePath = i.FilePath,
                                Price = i.Price,
                                HaveHardcopy = i.HaveHardcopy,
                                IsPurchased = p == null ? false: true,
                                IsInCart = c == null ? false : true,
                                AuthorName = u.FirstName + " " + u.LastName
                            };

                ImageVM[] results = query.ToArray();

                return View(results);
            }
        }

        private bool ImageExists(int id)
        {
          return _context.Images.Any(e => e.ID == id);
        }
    }
}
