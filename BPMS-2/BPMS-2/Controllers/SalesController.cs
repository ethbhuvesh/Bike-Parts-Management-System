using BPMS_2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
    public class SalesController : Controller
    {

        private readonly BPMS_2Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        public SalesController(BPMS_2Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.UserName;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CartModel.ToListAsync());
        }


        public async Task<IActionResult> UserRecord()
        {
            var UID = await GetCurrentUserId();
            var records=from record in _context.CartModel
                      where record.UID == UID
                      select record;

            return View(records);
        }
    }
}
