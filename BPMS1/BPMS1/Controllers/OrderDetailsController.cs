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
    public class OrderDetailsController : Controller
    {
        private readonly BPMS1Context _context;

        public OrderDetailsController(BPMS1Context context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.OrderDetailsModel.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.OrderDetailsModel == null)
            {
                return NotFound();
            }

            var orderDetailsModel = await _context.OrderDetailsModel
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetailsModel == null)
            {
                return NotFound();
            }

            return View(orderDetailsModel);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,ProductPrice,Quantity,UID,PurchaseDate")] OrderDetailsModel orderDetailsModel)
        {
            if (ModelState.IsValid)
            {
                orderDetailsModel.OrderId = Guid.NewGuid();
                _context.Add(orderDetailsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetailsModel);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.OrderDetailsModel == null)
            {
                return NotFound();
            }

            var orderDetailsModel = await _context.OrderDetailsModel.FindAsync(id);
            if (orderDetailsModel == null)
            {
                return NotFound();
            }
            return View(orderDetailsModel);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,ProductId,ProductPrice,Quantity,UID,PurchaseDate")] OrderDetailsModel orderDetailsModel)
        {
            if (id != orderDetailsModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetailsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsModelExists(orderDetailsModel.OrderId))
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
            return View(orderDetailsModel);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.OrderDetailsModel == null)
            {
                return NotFound();
            }

            var orderDetailsModel = await _context.OrderDetailsModel
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetailsModel == null)
            {
                return NotFound();
            }

            return View(orderDetailsModel);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.OrderDetailsModel == null)
            {
                return Problem("Entity set 'BPMS1Context.OrderDetailsModel'  is null.");
            }
            var orderDetailsModel = await _context.OrderDetailsModel.FindAsync(id);
            if (orderDetailsModel != null)
            {
                _context.OrderDetailsModel.Remove(orderDetailsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailsModelExists(Guid id)
        {
          return _context.OrderDetailsModel.Any(e => e.OrderId == id);
        }
    }
}
