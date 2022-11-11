using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BPMS_2.Data;
using BPMS_2.Models;
using Microsoft.AspNetCore.Authorization;

namespace BPMS_2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BPMS_2Context _context;
        private readonly ILogger<AccountController> _logger;

        public ProductsController(BPMS_2Context context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductsModel.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ProductsModel == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            return View(productsModel);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductId,ProductCategory,ProductDescription,ProductSize,InventoryCount,ProductPrice,BikePartImage")] ProductsModel productsModel)
        {
            if (ModelState.IsValid)
            {
                productsModel.ProductId = Guid.NewGuid();
                _context.Add(productsModel);
                await _context.SaveChangesAsync();
                string message = $"Product added in products database. Product ID - {productsModel.ProductId}.";
                _logger.LogInformation(message);
                return RedirectToAction(nameof(Index));
            }
            return View(productsModel);

        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ProductsModel == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModel.FindAsync(id);
            if (productsModel == null)
            {
                return NotFound();
            }
            return View(productsModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,ProductCategory,ProductDescription,ProductSize,InventoryCount,ProductPrice,BikePartImage")] ProductsModel productsModel)
        {
            if (id != productsModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productsModel);
                    await _context.SaveChangesAsync();
                    string message = $"Products database updated. Product ID - {productsModel.ProductId}.";
                    _logger.LogInformation(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsModelExists(productsModel.ProductId))
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
            return View(productsModel);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ProductsModel == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModel
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            return View(productsModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ProductsModel == null)
            {
                return Problem("Entity set 'BPMS_2Context.ProductsModel'  is null.");
            }
            var productsModel = await _context.ProductsModel.FindAsync(id);
            if (productsModel != null)
            {
                _context.ProductsModel.Remove(productsModel);
                string message = $"Product removed from products database. Product ID - {productsModel.ProductId}.";
                _logger.LogInformation(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsModelExists(Guid id)
        {
          return _context.ProductsModel.Any(e => e.ProductId == id);
        }
    }
}
