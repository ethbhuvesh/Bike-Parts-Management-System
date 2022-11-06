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
    public class UsersController : Controller
    {
        private readonly BPMS_2Context _context;

        public UsersController(BPMS_2Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return View(await _context.UsersModel.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.UsersModel == null)
            {
                return NotFound();
            }

            var usersModel = await _context.UsersModel
                .FirstOrDefaultAsync(m => m.UID == id);
            if (usersModel == null)
            {
                return NotFound();
            }

            return View(usersModel);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UID,FirstName,LastName,Email,Password,Role")] UsersModel usersModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UsersModel == null)
            {
                return NotFound();
            }

            var usersModel = await _context.UsersModel.FindAsync(id);
            if (usersModel == null)
            {
                return NotFound();
            }
            return View(usersModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UID,FirstName,LastName,Email,Password,Role")] UsersModel usersModel)
        {
            if (id != usersModel.UID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersModelExists(usersModel.UID))
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
            return View(usersModel);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.UsersModel == null)
            {
                return NotFound();
            }

            var usersModel = await _context.UsersModel
                .FirstOrDefaultAsync(m => m.UID == id);
            if (usersModel == null)
            {
                return NotFound();
            }

            return View(usersModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.UsersModel == null)
            {
                return Problem("Entity set 'BPMS_2Context.UsersModel'  is null.");
            }
            var usersModel = await _context.UsersModel.FindAsync(id);
            if (usersModel != null)
            {
                _context.UsersModel.Remove(usersModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersModelExists(string id)
        {
          return _context.UsersModel.Any(e => e.UID == id);
        }
    }
}
