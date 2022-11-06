using BPMS_2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
    public class SalesController : Controller
    {

        private readonly BPMS_2Context _context;

        public SalesController(BPMS_2Context context)
        {
            _context = context;
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CartModel.ToListAsync());
        }
    }
}
