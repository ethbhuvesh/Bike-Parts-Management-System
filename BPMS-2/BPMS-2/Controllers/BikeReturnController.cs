using BPMS_2.Data;
using BPMS_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
	[Authorize]
    public class BikeReturnController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly BPMS_2Context _context;
        private readonly ILogger<AccountController> _logger;
        public BikeReturnController(BPMS_2Context context, UserManager<IdentityUser> userManager, ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.UserName;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Index()
        {
            var UID = await GetCurrentUserId();
            var orderdetails = from orderdetail in _context.OrderDetailsModel
                               where orderdetail.UID == UID
                               where orderdetail.Returned == false
                               where orderdetail.ProductCategory == "Rent"
                               select orderdetail;


            return View(orderdetails);

        }

     
        public OrderDetailsModel GetProductById(Guid productId)
        {

            return _context.OrderDetailsModel.FirstOrDefault(p => p.ProductId == productId && p.Returned==false);

        }


        [HttpPost("{productId}/BikeReturn")]
        public async Task<IActionResult> Return(Guid productId)
        {
           
            var order = GetProductById(productId);
            if(order.ReturnDate>=DateTime.UtcNow)
            {
                ViewBag.message = 0;
              
            }
            else
            {
                var diff = (DateTime.UtcNow - order.ReturnDate).Days;
                decimal fineMoney = diff * 10;
                ViewBag.message=fineMoney.ToString();
            }
            

            order.Returned = true;
            await _context.SaveChangesAsync();

            string message = $"Bike returned by {await GetCurrentUserId()}";
            _logger.LogInformation(message);
            return View();

        }
    }
}
