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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> Index()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //return View(await _context.PurchasedItems.ToListAsync());

            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");

            }
            else
            {
                // Have login
             
#pragma warning disable CS8629 // Nullable value type may be null.
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
#pragma warning restore CS8629 // Nullable value type may be null.

                /*
                var purchasedItem = await _context.PurchasedItems.Where(m => m.UserId == UserID).ToListAsync();
                
                if (purchasedItem == null)
                {
                    return NotFound();
                }
                
                return View(purchasedItem);
                */

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
