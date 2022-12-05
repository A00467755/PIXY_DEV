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
              return View(await _context.PurchasedItems.ToListAsync());
        }

        // GET: PurchasedItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchasedItems == null)
            {
                return NotFound();
            }

            var purchasedItem = await _context.PurchasedItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchasedItem == null)
            {
                return NotFound();
            }

            return View(purchasedItem);
        }

        // GET: PurchasedItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchasedItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserId,ImageId,IsHardcopy,NoOfHardcopy,TransactionId")] PurchasedItem purchasedItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchasedItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchasedItem);
        }

        // GET: PurchasedItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchasedItems == null)
            {
                return NotFound();
            }

            var purchasedItem = await _context.PurchasedItems.FindAsync(id);
            if (purchasedItem == null)
            {
                return NotFound();
            }
            return View(purchasedItem);
        }

        // POST: PurchasedItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserId,ImageId,IsHardcopy,NoOfHardcopy,TransactionId")] PurchasedItem purchasedItem)
        {
            if (id != purchasedItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchasedItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasedItemExists(purchasedItem.ID))
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
            return View(purchasedItem);
        }

        // GET: PurchasedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchasedItems == null)
            {
                return NotFound();
            }

            var purchasedItem = await _context.PurchasedItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchasedItem == null)
            {
                return NotFound();
            }

            return View(purchasedItem);
        }

        // POST: PurchasedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchasedItems == null)
            {
                return Problem("Entity set 'PIXYContext.PurchasedItem'  is null.");
            }
            var purchasedItem = await _context.PurchasedItems.FindAsync(id);
            if (purchasedItem != null)
            {
                _context.PurchasedItems.Remove(purchasedItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasedItemExists(int id)
        {
          return _context.PurchasedItems.Any(e => e.ID == id);
        }
    }
}
