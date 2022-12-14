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
    public class PurchasedItemsController : Controller
    {
        private readonly PIXYContext _context;

        public PurchasedItemsController(PIXYContext context)
        {
            _context = context;
        }

        // GET: PurchasedItems
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");

            }
            else
            {
                // Have login
             
                int UserID = (int)HttpContext.Session.GetInt32("UserID");

                var query = from p in _context.PurchasedItems
                            join i in _context.Images on p.ImageId equals i.ID
                            where p.UserId == UserID
                            select new PurchasedItemVM
                            {
                                ID = p.ID,
                                UserId = p.UserId,
                                ImageId = p.ImageId,
                                IsHardcopy = p.IsHardcopy,
                                NoOfHardcopy = p.NoOfHardcopy,
                                FilePath = i.FilePath
                            };

                PurchasedItemVM[] results = query.ToArray();

                return View(results);
            }
        }

        private bool PurchasedItemExists(int id)
        {
          return _context.PurchasedItems.Any(e => e.ID == id);
        }
    }
}
