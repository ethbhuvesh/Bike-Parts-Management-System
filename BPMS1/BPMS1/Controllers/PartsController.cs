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
    public class PartsController : Controller
    {
        private readonly BPMS1Context _context;

        public PartsController(BPMS1Context context)
        {
            _context = context;
        }

        // GET: Parts
        public async Task<IActionResult> Index()
        {
              return View(await _context.PartsModel.ToListAsync());
        }

        // GET: Parts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PartsModel == null)
            {
                return NotFound();
            }

            var partsModel = await _context.PartsModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (partsModel == null)
            {
                return NotFound();
            }

            return View(partsModel);
        }

        // GET: Parts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductCount,ProductPrice")] PartsModel partsModel)
        {
            if (ModelState.IsValid)
            {
                partsModel.ProductId = Guid.NewGuid();
                _context.Add(partsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partsModel);
        }

        // GET: Parts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PartsModel == null)
            {
                return NotFound();
            }

            var partsModel = await _context.PartsModel.FindAsync(id);
            if (partsModel == null)
            {
                return NotFound();
            }
            return View(partsModel);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,ProductName,ProductCount,ProductPrice")] PartsModel partsModel)
        {
            if (id != partsModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartsModelExists(partsModel.ProductId))
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
            return View(partsModel);
        }

        // GET: Parts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PartsModel == null)
            {
                return NotFound();
            }

            var partsModel = await _context.PartsModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (partsModel == null)
            {
                return NotFound();
            }

            return View(partsModel);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PartsModel == null)
            {
                return Problem("Entity set 'BPMS1Context.PartsModel'  is null.");
            }
            var partsModel = await _context.PartsModel.FindAsync(id);
            if (partsModel != null)
            {
                _context.PartsModel.Remove(partsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartsModelExists(Guid id)
        {
          return _context.PartsModel.Any(e => e.ProductId == id);
        }
    }
}
