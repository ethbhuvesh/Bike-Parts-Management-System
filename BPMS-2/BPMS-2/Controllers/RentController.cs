using BPMS_2.Data;
using BPMS_2.Models;
using LearnASPNETCoreMVCWithRealApps.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
    public class RentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.RentBikesModel.ToListAsync());
        }

        private readonly BPMS_2Context _context;
        public RentController(BPMS_2Context context)
        {
            _context = context;
        }

        public RentBikesModel GetProductById(Guid productId)
        {
            return _context.RentBikesModel.SingleOrDefault(p => p.ProductId == productId);
        }


        public IActionResult FinalCart()
        {
            var rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
            ViewBag.rentcart = rentcart;
            ViewBag.total = rentcart.Sum(item => item.TotalPrice);
            
            return View();
        }



        //[HttpPost("{productId}")]
        public async Task<IActionResult> RentProduct(Guid productId, int quantity = 1)
        {
            var product = GetProductById(productId);
            if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart") == null)
            {
                List<OrderDetailsModel> rentcart = new List<OrderDetailsModel>();
                if (quantity < product.InventoryCount)
                {
                    rentcart.Add(new OrderDetailsModel
                    {
                        Quantity = quantity,
                        ProductId = productId,
                        ProductPrice = product.ProductPrice
                    });
                    product.InventoryCount -= quantity;

                    await _context.SaveChangesAsync();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "rentcart", rentcart);

                }
            }

            else
            {
                List<OrderDetailsModel> cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
                int index = isExist(productId);
                if (index != -1 && product.InventoryCount > quantity)
                {
                    cart[index].Quantity += quantity;
                    product.InventoryCount -= quantity;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (product.InventoryCount > quantity)
                    {
                        cart.Add(new OrderDetailsModel
                        {
                            Quantity = quantity,
                            ProductId = productId,
                            ProductPrice = product.ProductPrice
                        });
                        product.InventoryCount -= quantity;

                        await _context.SaveChangesAsync();

                    }

                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("FinalCart");
        }





        public async Task<IActionResult> Checkout()
        {

            if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart") == null)
            {
                return RedirectToAction("Rent", "Index");
            }
            else
            {
                var rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
                CartModel finalcart = new CartModel();
                finalcart.UID = "test_id";
                finalcart.OrderId = Guid.NewGuid();
                finalcart.OrderDate = DateTime.UtcNow;
                finalcart.SubTotal = rentcart.Sum(item => item.TotalPrice);
                finalcart.ReturnDate = DateTime.Now.AddMonths(6);
                _context.CartModel.Add(finalcart);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Success");

        }



        [HttpPost]
        public IActionResult Remove(Guid productId)
        {
            List<OrderDetailsModel> rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
            int index = isExist(productId);
            rentcart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "rentcart", rentcart);
            return RedirectToAction("FinalCart");
        }


        private int isExist(Guid productId)
        {
            List<OrderDetailsModel> rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
            for (int i = 0; i < rentcart.Count; i++)
            {
                if (rentcart[i].ProductId.Equals(productId))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
