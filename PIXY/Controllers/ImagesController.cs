﻿using System;
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


        // GET: Images/Buy/5
        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ID == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }


        // GET: Images
        public async Task<IActionResult> Index(string ImageCategory, string SearchString)
        {
            IQueryable<string> CategoryQuery = from m in _context.Images
                                            orderby m.CategoryDesc
                                            select m.CategoryDesc;
            
            var FilteredImages = from i in _context.Images
                                 select i;

            if (!string.IsNullOrEmpty(ImageCategory))
            {
                FilteredImages = FilteredImages.Where(s => s.CategoryDesc == ImageCategory);
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                FilteredImages = FilteredImages.Where(s => s.ImageTags!.Contains(SearchString));
            }

            var imageCategoryVM = new ImagesCategoryView
            {
                Categories = new SelectList(await CategoryQuery.Distinct().ToListAsync()),
                Images = await FilteredImages.ToListAsync()
            };
            return View(imageCategoryVM);
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ID == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserId,CategoryDesc,ImageType,FilePathWatermark,FilePath,Price,HaveHardcopy")] Image image)
        {
            if (ModelState.IsValid)
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserId,CategoryDesc,ImageType,FilePathWatermark,FilePath,Price,HaveHardcopy")] Image image)
        {
            if (id != image.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.ID))
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
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ID == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'PIXYContext.Images'  is null.");
            }
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return _context.Images.Any(e => e.ID == id);
        }
    }
}
