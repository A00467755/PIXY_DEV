using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;

namespace PIXY.Controllers
{
    public class CartsController : Controller
    {
        private readonly PIXYContext _context;

        public CartsController(PIXYContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Carts.ToListAsync());
        }

        public async Task<IActionResult> AddToCart(int ImageId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");

            }
            else {
                // Haven login

                int UserID = (int)HttpContext.Session.GetInt32("UserID");

                Cart c = new Cart();
                c.UserId = UserID;
                c.ImageId = ImageId;
                c.IsHardcopy = false;
                c.NoOfHardcopy = 0;

                _context.Add(c);
                await _context.SaveChangesAsync();

               // TempData["AddedToCart"] = "Images added to cart successfully";
                return RedirectToAction("Index", "Images");
            }
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity is null.");
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromCartByImageId(int ImageId)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity is null.");
            }

            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");

            }
            else
            {
                // Haven login

                int UserID = (int)HttpContext.Session.GetInt32("UserID");

                var x = (from c in _context.Carts
                         where c.UserId == UserID && c.ImageId == ImageId
                         orderby c.ID descending
                         select c).FirstOrDefault();

                if (x != null) { 
                    _context.Carts.Remove(x);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Images");
        }

        public async Task<IActionResult> ConfirmPurchase()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");

            }
            else
            {
                // Have login

                //int UserID = 1; // login user ID to changed
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
                // Copy Cart content to transaction and purchased items and remove cart item

                List<Cart> carts = await _context.Carts.Where(m => m.UserId == UserID).ToListAsync();

                if (carts != null)
                {
                    Transaction Transactions = new Transaction();
                    PurchasedItem PurchasedItems = new PurchasedItem();
                    foreach (var c in carts)
                    {
                        Transaction t = new Transaction();
                        PurchasedItem p = new PurchasedItem();
                        t.UserId = c.UserId;
                        t.ImageId = c.ImageId;
                        t.IsHardcopy = c.IsHardcopy;
                        t.NoOfHardcopy = c.NoOfHardcopy;
                        t.PurchaseDataTime = DateTime.Now;

                        p.UserId = c.UserId;
                        p.ImageId = c.ImageId;
                        p.IsHardcopy = c.IsHardcopy;
                        p.NoOfHardcopy = c.NoOfHardcopy;

                        // Add to transaction and purchased items
                        _context.Add(t);
                        _context.Add(p);

                        // Clear Shopping Cart
                        _context.Remove(c);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("PurchaseSuccess", "Carts");
            }
        }

        public IActionResult PurchaseSuccess()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserId,ImageId,IsHardcopy,NoOfHardcopy")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserId,ImageId,IsHardcopy,NoOfHardcopy")] Cart cart)
        {
            if (id != cart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.ID))
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
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'PIXYContext.Carts'  is null.");
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return _context.Carts.Any(e => e.ID == id);
        }

        public bool ValidateCard(string CardNo, string CardType)
        {
            if (!long.TryParse(CardNo, out _))
            {
                return false;
            }

            if (CardNo.Length < 15 && CardNo.Length > 16)
            {
                return false;
            }
            if (CardType.Equals("Visa"))
            {
                if (!(CardNo.StartsWith("4") && CardNo.Length == 16))
                {
                    return false;
                }
            }
            if (CardType.Equals("MasterCard"))
            {
                int CardNumberPrefix = int.Parse(CardNo.Substring(0,2));
                if (!((CardNumberPrefix >= 51 && CardNumberPrefix <= 55) && CardNo.Length == 16))
                {
                    return false;
                }
            }
            if (CardType.Equals("Amex"))
            {
                if (CardNo.Length != 15)
                {
                    return false;
                }
                if (!(CardNo.StartsWith("34") || CardNo.StartsWith("37")))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
