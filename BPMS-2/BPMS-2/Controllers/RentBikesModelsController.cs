using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPMS_2.Data;
using BPMS_2.Models;

namespace BPMS_2.Controllers
{
    public class RentBikesModelsController : Controller
    {
        private readonly BPMS_2Context _context;

        public RentBikesModelsController(BPMS_2Context context)
        {
            _context = context;
        }

        // GET: RentBikesModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.RentBikesModel.ToListAsync());
        }

        // GET: RentBikesModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.RentBikesModel == null)
            {
                return NotFound();
            }

            var rentBikesModel = await _context.RentBikesModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (rentBikesModel == null)
            {
                return NotFound();
            }

            return View(rentBikesModel);
        }

        // GET: RentBikesModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RentBikesModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductCategory,ProductDescription,ProductSize,InventoryCount,ProductPrice")] RentBikesModel rentBikesModel)
        {
            if (ModelState.IsValid)
            {
                rentBikesModel.ProductId = Guid.NewGuid();
                _context.Add(rentBikesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentBikesModel);
        }

        // GET: RentBikesModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.RentBikesModel == null)
            {
                return NotFound();
            }

            var rentBikesModel = await _context.RentBikesModel.FindAsync(id);
            if (rentBikesModel == null)
            {
                return NotFound();
            }
            return View(rentBikesModel);
        }

        // POST: RentBikesModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,ProductCategory,ProductDescription,ProductSize,InventoryCount,ProductPrice")] RentBikesModel rentBikesModel)
        {
            if (id != rentBikesModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentBikesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentBikesModelExists(rentBikesModel.ProductId))
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
            return View(rentBikesModel);
        }

        // GET: RentBikesModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.RentBikesModel == null)
            {
                return NotFound();
            }

            var rentBikesModel = await _context.RentBikesModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (rentBikesModel == null)
            {
                return NotFound();
            }

            return View(rentBikesModel);
        }

        // POST: RentBikesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.RentBikesModel == null)
            {
                return Problem("Entity set 'BPMS_2Context.RentBikesModel'  is null.");
            }
            var rentBikesModel = await _context.RentBikesModel.FindAsync(id);
            if (rentBikesModel != null)
            {
                _context.RentBikesModel.Remove(rentBikesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentBikesModelExists(Guid id)
        {
          return _context.RentBikesModel.Any(e => e.ProductId == id);
        }
    }
}
