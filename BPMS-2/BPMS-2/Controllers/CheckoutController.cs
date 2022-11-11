using BPMS_2.Data;
using BPMS_2.Models;
using LearnASPNETCoreMVCWithRealApps.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BPMS_2.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly BPMS_2Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;


        public CheckoutController(UserManager<IdentityUser> userManager,
        BPMS_2Context context,
        ILogger<AccountController> logger)
        {
            _userManager = userManager;

            _context = context;
            _logger = logger;
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.UserName;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            
            if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart") == null)
            {
                return RedirectToAction("Cart","Index");
            }
            else
            {
                var cart= SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart");
                CartModel finalcart = new CartModel();
                finalcart.UID = await GetCurrentUserId();
                finalcart.OrderId = Guid.NewGuid();
                finalcart.OrderDate = cart[0].OrderDate;
                finalcart.SubTotal = cart.Sum(item=>item.TotalPrice);
                finalcart.ReturnDate = null;

                finalcart.OrderDetails = cart;

                _context.CartModel.Add(finalcart);
                await _context.SaveChangesAsync();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);

            }
            string message = $"Order placed by user {await GetCurrentUserId()}";
            _logger.LogInformation(message);

            return RedirectToAction("Index");
            
        }
    }
}
