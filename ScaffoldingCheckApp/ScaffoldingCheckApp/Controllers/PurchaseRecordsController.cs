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
    public class PurchaseRecordsController : Controller
    {
        private readonly ScaffoldingCheckAppContext _context;

        public PurchaseRecordsController(ScaffoldingCheckAppContext context)
        {
            _context = context;
        }
        
        // GET: PurchaseRecords
        public async Task<IActionResult> Index()
        {
              return View(await _context.PurchaseRecord.ToListAsync());
        }

        // GET: PurchaseRecords/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PurchaseRecord == null)
            {
                return NotFound();
            }

            var purchaseRecord = await _context.PurchaseRecord
                .FirstOrDefaultAsync(m => m.UID == id);
            if (purchaseRecord == null)
            {
                return NotFound();
            }

            return View(purchaseRecord);
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
        public async Task<IActionResult> Create([Bind("UID,PurchaseType,RentDate,DueDate,BikeReturnDate,ProductPurchased,TotalAmount")] PurchaseRecord purchaseRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseRecord);
        }

        // GET: PurchaseRecords/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PurchaseRecord == null)
            {
                return NotFound();
            }

            var purchaseRecord = await _context.PurchaseRecord.FindAsync(id);
            if (purchaseRecord == null)
            {
                return NotFound();
            }
            return View(purchaseRecord);
        }

        // POST: PurchaseRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UID,PurchaseType,RentDate,DueDate,BikeReturnDate,ProductPurchased,TotalAmount")] PurchaseRecord purchaseRecord)
        {
            if (id != purchaseRecord.UID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseRecordExists(purchaseRecord.UID))
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
            return View(purchaseRecord);
        }

        // GET: PurchaseRecords/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PurchaseRecord == null)
            {
                return NotFound();
            }

            var purchaseRecord = await _context.PurchaseRecord
                .FirstOrDefaultAsync(m => m.UID == id);
            if (purchaseRecord == null)
            {
                return NotFound();
            }

            return View(purchaseRecord);
        }

        // POST: PurchaseRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PurchaseRecord == null)
            {
                return Problem("Entity set 'ScaffoldingCheckAppContext.PurchaseRecord'  is null.");
            }
            var purchaseRecord = await _context.PurchaseRecord.FindAsync(id);
            if (purchaseRecord != null)
            {
                _context.PurchaseRecord.Remove(purchaseRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseRecordExists(string id)
        {
          return _context.PurchaseRecord.Any(e => e.UID == id);
        }
    }
}
