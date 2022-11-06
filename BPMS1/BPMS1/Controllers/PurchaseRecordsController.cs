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
    public class PurchaseRecordsController : Controller
    {
        private readonly BPMS1Context _context;

        public PurchaseRecordsController(BPMS1Context context)
        {
            _context = context;
        }

        // GET: PurchaseRecords
        public async Task<IActionResult> Index()
        {
              return View(await _context.PurchaseRecordsModel.ToListAsync());
        }

        // GET: PurchaseRecords/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PurchaseRecordsModel == null)
            {
                return NotFound();
            }

            var purchaseRecordsModel = await _context.PurchaseRecordsModel
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (purchaseRecordsModel == null)
            {
                return NotFound();
            }

            return View(purchaseRecordsModel);
        }

        // GET: PurchaseRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UID,ProductId,ProductName,PurchaseDate,PurchaseAmount")] PurchaseRecordsModel purchaseRecordsModel)
        {
            if (ModelState.IsValid)
            {
                purchaseRecordsModel.OrderId = Guid.NewGuid();
                _context.Add(purchaseRecordsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseRecordsModel);
        }

        // GET: PurchaseRecords/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PurchaseRecordsModel == null)
            {
                return NotFound();
            }

            var purchaseRecordsModel = await _context.PurchaseRecordsModel.FindAsync(id);
            if (purchaseRecordsModel == null)
            {
                return NotFound();
            }
            return View(purchaseRecordsModel);
        }

        // POST: PurchaseRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,UID,ProductId,ProductName,PurchaseDate,PurchaseAmount")] PurchaseRecordsModel purchaseRecordsModel)
        {
            if (id != purchaseRecordsModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseRecordsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseRecordsModelExists(purchaseRecordsModel.OrderId))
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
            return View(purchaseRecordsModel);
        }

        // GET: PurchaseRecords/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PurchaseRecordsModel == null)
            {
                return NotFound();
            }

            var purchaseRecordsModel = await _context.PurchaseRecordsModel
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (purchaseRecordsModel == null)
            {
                return NotFound();
            }

            return View(purchaseRecordsModel);
        }

        // POST: PurchaseRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PurchaseRecordsModel == null)
            {
                return Problem("Entity set 'BPMS1Context.PurchaseRecordsModel'  is null.");
            }
            var purchaseRecordsModel = await _context.PurchaseRecordsModel.FindAsync(id);
            if (purchaseRecordsModel != null)
            {
                _context.PurchaseRecordsModel.Remove(purchaseRecordsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseRecordsModelExists(Guid id)
        {
          return _context.PurchaseRecordsModel.Any(e => e.OrderId == id);
        }
    }
}
