using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScaffoldingCheckApp.Data;
using ScaffoldingCheckApp.Models;

namespace ScaffoldingCheckApp.Controllers
{
    public class BikesInventoryController : Controller
    {
        private readonly ScaffoldingCheckAppContext _context;

        public BikesInventoryController(ScaffoldingCheckAppContext context)
        {
            _context = context;
        }

        // GET: BikesInventory
        public async Task<IActionResult> Index()
        {
              return View(await _context.BikesInventory.ToListAsync());
        }

        // GET: BikesInventory/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.BikesInventory == null)
            {
                return NotFound();
            }

            var bikesInventory = await _context.BikesInventory
                .FirstOrDefaultAsync(m => m.BikeNumber == id);
            if (bikesInventory == null)
            {
                return NotFound();
            }

            return View(bikesInventory);
        }

        // GET: BikesInventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BikesInventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BikeNumber,ModelName,BikeSize,BikeCount")] BikesInventory bikesInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bikesInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikesInventory);
        }

        // GET: BikesInventory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.BikesInventory == null)
            {
                return NotFound();
            }

            var bikesInventory = await _context.BikesInventory.FindAsync(id);
            if (bikesInventory == null)
            {
                return NotFound();
            }
            return View(bikesInventory);
        }

        // POST: BikesInventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BikeNumber,ModelName,BikeSize,BikeCount")] BikesInventory bikesInventory)
        {
            if (id != bikesInventory.BikeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikesInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikesInventoryExists(bikesInventory.BikeNumber))
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
            return View(bikesInventory);
        }

        // GET: BikesInventory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.BikesInventory == null)
            {
                return NotFound();
            }

            var bikesInventory = await _context.BikesInventory
                .FirstOrDefaultAsync(m => m.BikeNumber == id);
            if (bikesInventory == null)
            {
                return NotFound();
            }

            return View(bikesInventory);
        }

        // POST: BikesInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.BikesInventory == null)
            {
                return Problem("Entity set 'ScaffoldingCheckAppContext.BikesInventory'  is null.");
            }
            var bikesInventory = await _context.BikesInventory.FindAsync(id);
            if (bikesInventory != null)
            {
                _context.BikesInventory.Remove(bikesInventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikesInventoryExists(string id)
        {
          return _context.BikesInventory.Any(e => e.BikeNumber == id);
        }
    }
}
