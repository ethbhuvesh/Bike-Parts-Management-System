using BPMS1.Data;
using BPMS1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS1.Controllers
{
    public class CartController : Controller
    {
        private readonly BPMS1Context _context;
        public CartController(BPMS1Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.BikesModel.ToListAsync());
        }

        public BikesModel GetProductById(Guid productId)
        {
            return _context.BikesModel.SingleOrDefault(p => p.ProductId == productId);
        }

        

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddOrder(Guid productId, int quantity)
        {
            var cart = new Cart();

            var product = GetProductById(productId);
            //var orderid = System.Guid.NewGuid();
            var orderDetail = new OrderDetailsModel()
            {

                UID="test",
                ProductId = productId,
                Quantity = quantity,
                PurchaseDate=DateTime.UtcNow,
                ProductPrice = product.ProductPrice
                
            };
            cart.OrderDetails.Add(orderDetail);
            
            _context.OrderDetailsModel.Add(orderDetail);    
            
            await _context.SaveChangesAsync();
            
            

            return RedirectToAction("Index1");
        }

        public async Task<IActionResult> Index1()
        {
            return View();
        }
    }
}
