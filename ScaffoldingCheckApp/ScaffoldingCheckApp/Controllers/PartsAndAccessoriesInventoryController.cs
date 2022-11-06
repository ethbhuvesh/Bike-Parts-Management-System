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
    public class PartsAndAccessoriesInventoryController : Controller
    {
        private readonly ScaffoldingCheckAppContext _context;

        public PartsAndAccessoriesInventoryController(ScaffoldingCheckAppContext context)
        {
            _context = context;
        }

        // GET: PartsAndAccessoriesInventory
        public async Task<IActionResult> Index()
        {
              return View(await _context.PartsAndAccessoriesInventory.ToListAsync());
        }

        // GET: PartsAndAccessoriesInventory/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PartsAndAccessoriesInventory == null)
            {
                return NotFound();
            }

            var partsAndAccessoriesInventory = await _context.PartsAndAccessoriesInventory
                .FirstOrDefaultAsync(m => m.ItemNumber == id);
            if (partsAndAccessoriesInventory == null)
            {
                return NotFound();
            }

            return View(partsAndAccessoriesInventory);
        }

        // GET: PartsAndAccessoriesInventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartsAndAccessoriesInventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemNumber,ItemName,ItemCount")] PartsAndAccessoriesInventory partsAndAccessoriesInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partsAndAccessoriesInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partsAndAccessoriesInventory);
        }

        // GET: PartsAndAccessoriesInventory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PartsAndAccessoriesInventory == null)
            {
                return NotFound();
            }

            var partsAndAccessoriesInventory = await _context.PartsAndAccessoriesInventory.FindAsync(id);
            if (partsAndAccessoriesInventory == null)
            {
                return NotFound();
            }
            return View(partsAndAccessoriesInventory);
        }

        // POST: PartsAndAccessoriesInventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemNumber,ItemName,ItemCount")] PartsAndAccessoriesInventory partsAndAccessoriesInventory)
        {
            if (id != partsAndAccessoriesInventory.ItemNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partsAndAccessoriesInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartsAndAccessoriesInventoryExists(partsAndAccessoriesInventory.ItemNumber))
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
            return View(partsAndAccessoriesInventory);
        }

        // GET: PartsAndAccessoriesInventory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PartsAndAccessoriesInventory == null)
            {
                return NotFound();
            }

            var partsAndAccessoriesInventory = await _context.PartsAndAccessoriesInventory
                .FirstOrDefaultAsync(m => m.ItemNumber == id);
            if (partsAndAccessoriesInventory == null)
            {
                return NotFound();
            }

            return View(partsAndAccessoriesInventory);
        }

        // POST: PartsAndAccessoriesInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PartsAndAccessoriesInventory == null)
            {
                return Problem("Entity set 'ScaffoldingCheckAppContext.PartsAndAccessoriesInventory'  is null.");
            }
            var partsAndAccessoriesInventory = await _context.PartsAndAccessoriesInventory.FindAsync(id);
            if (partsAndAccessoriesInventory != null)
            {
                _context.PartsAndAccessoriesInventory.Remove(partsAndAccessoriesInventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartsAndAccessoriesInventoryExists(string id)
        {
          return _context.PartsAndAccessoriesInventory.Any(e => e.ItemNumber == id);
        }
    }
}
