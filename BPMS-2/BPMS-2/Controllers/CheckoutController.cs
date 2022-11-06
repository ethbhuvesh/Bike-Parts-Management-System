using BPMS_2.Data;
using BPMS_2.Models;
using LearnASPNETCoreMVCWithRealApps.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BPMS_2.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly BPMS_2Context _context;
        public CheckoutController(BPMS_2Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        
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
                finalcart.UID = "test_id";
                finalcart.OrderId = Guid.NewGuid();
                finalcart.OrderDate = DateTime.UtcNow;
                finalcart.SubTotal = cart.Sum(item=>item.TotalPrice);
                finalcart.ReturnDate = null;
                _context.CartModel.Add(finalcart);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");
            
        }
    }
}
