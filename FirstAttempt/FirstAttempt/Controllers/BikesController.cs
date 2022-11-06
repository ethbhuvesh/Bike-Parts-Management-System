using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstAttempt.Data;
using FirstAttempt.Models;

namespace FirstAttempt.Controllers
{
    public class BikesController : Controller
    {
        private readonly FirstAttemptContext _context;

        public BikesController(FirstAttemptContext context)
        {
            _context = context;
        }

        // GET: Bikes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Bikes.ToListAsync());
        }

        // GET: Bikes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Bikes == null)
            {
                return NotFound();
            }

            var bikes = await _context.Bikes
                .FirstOrDefaultAsync(m => m.BikeId == id);
            if (bikes == null)
            {
                return NotFound();
            }

            return View(bikes);
        }

        // GET: Bikes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BikeId,BikeName,BikeSize,BikePrice,BikeCount")] Bikes bikes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bikes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikes);
        }

        // GET: Bikes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Bikes == null)
            {
                return NotFound();
            }

            var bikes = await _context.Bikes.FindAsync(id);
            if (bikes == null)
            {
                return NotFound();
            }
            return View(bikes);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BikeId,BikeName,BikeSize,BikePrice,BikeCount")] Bikes bikes)
        {
            if (id != bikes.BikeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikesExists(bikes.BikeId))
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
            return View(bikes);
        }

        // GET: Bikes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Bikes == null)
            {
                return NotFound();
            }

            var bikes = await _context.Bikes
                .FirstOrDefaultAsync(m => m.BikeId == id);
            if (bikes == null)
            {
                return NotFound();
            }

            return View(bikes);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Bikes == null)
            {
                return Problem("Entity set 'FirstAttemptContext.Bikes'  is null.");
            }
            var bikes = await _context.Bikes.FindAsync(id);
            if (bikes != null)
            {
                _context.Bikes.Remove(bikes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikesExists(string id)
        {
          return _context.Bikes.Any(e => e.BikeId == id);
        }
    }
}
