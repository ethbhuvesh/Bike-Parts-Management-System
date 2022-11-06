using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPMS1.Data;
using BPMS1.Models;

namespace BPMS1.Controllers
{
    public class BikesController : Controller
    {
        private readonly BPMS1Context _context;

        public BikesController(BPMS1Context context)
        {
            _context = context;
        }

        // GET: Bikes
        public async Task<IActionResult> Index()
        {
              return View(await _context.BikesModel.ToListAsync());
        }

        // GET: Bikes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.BikesModel == null)
            {
                return NotFound();
            }

            var bikesModel = await _context.BikesModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (bikesModel == null)
            {
                return NotFound();
            }

            return View(bikesModel);
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
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductSize,ProductCount,ProductPrice")] BikesModel bikesModel)
        {
            if (ModelState.IsValid)
            {
                bikesModel.ProductId = Guid.NewGuid();
                _context.Add(bikesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bikesModel);
        }

        // GET: Bikes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.BikesModel == null)
            {
                return NotFound();
            }

            var bikesModel = await _context.BikesModel.FindAsync(id);
            if (bikesModel == null)
            {
                return NotFound();
            }
            return View(bikesModel);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,ProductName,ProductSize,ProductCount,ProductPrice")] BikesModel bikesModel)
        {
            if (id != bikesModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bikesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikesModelExists(bikesModel.ProductId))
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
            return View(bikesModel);
        }

        // GET: Bikes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.BikesModel == null)
            {
                return NotFound();
            }

            var bikesModel = await _context.BikesModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (bikesModel == null)
            {
                return NotFound();
            }

            return View(bikesModel);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.BikesModel == null)
            {
                return Problem("Entity set 'BPMS1Context.BikesModel'  is null.");
            }
            var bikesModel = await _context.BikesModel.FindAsync(id);
            if (bikesModel != null)
            {
                _context.BikesModel.Remove(bikesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikesModelExists(Guid id)
        {
          return _context.BikesModel.Any(e => e.ProductId == id);
        }
    }
}
