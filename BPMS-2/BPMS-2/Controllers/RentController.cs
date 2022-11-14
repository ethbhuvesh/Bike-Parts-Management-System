using BPMS_2.Data;
using BPMS_2.Models;
using LearnASPNETCoreMVCWithRealApps.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
    public class RentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BPMS_2Context _context;
        private readonly ILogger<AccountController> _logger;
        public RentController(BPMS_2Context context, UserManager<IdentityUser> userManager, ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.RentBikesModel.ToListAsync());
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.UserName;
        }

        public IActionResult Failure()
        {
            return View();
        }

        public RentBikesModel GetProductById(Guid productId)
        {
            return _context.RentBikesModel.SingleOrDefault(p => p.ProductId == productId);
        }

        [Authorize]
        public IActionResult FinalCart()
        {
            var rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
            ViewBag.rentcart = rentcart;
            ViewBag.total = rentcart.Sum(item => item.TotalPrice);
            
            return View();
        }



        //[HttpPost("{productId}")]
        [Authorize]
        public async Task<IActionResult> RentProduct(Guid productId, int quantity = 1)
        {
            var existingrents=from order in _context.OrderDetailsModel
                              where order.Returned == false
                              where order.ProductCategory == "Rent"
                              select order;

            if (existingrents.Any())
            {
                return View("NoRent", "Rent");
            }
            else
            {
                var product = GetProductById(productId);
                if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart") == null)
                {
                    List<OrderDetailsModel> rentcart = new List<OrderDetailsModel>();
                    if (quantity <= product.InventoryCount)
                    {

                        rentcart.Add(new OrderDetailsModel
                        {
                            Quantity = quantity,
                            ProductId = productId,
                            ProductPrice = product.ProductPrice,
                            ProductCategory = product.ProductCategory,
                            Returned = false,
                            OrderDate = DateTime.Now,
                            ReturnDate = DateTime.Now.AddMonths(6),
                            UID = await GetCurrentUserId(),
                            BikesPartsImage=product.RentalBikeImage
                        });
                        product.InventoryCount -= quantity;

                        await _context.SaveChangesAsync();
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "rentcart", rentcart);
                        return RedirectToAction("FinalCart");
                    }
                    else
                    {
                        return View("Failure");
                    }
                }

                else
                {
                    List<OrderDetailsModel> cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
                    int index = isExist(productId);

                    if (index != -1 && product.InventoryCount >= quantity)
                    {
                        cart[index].Quantity += quantity;
                        product.InventoryCount -= quantity;
                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        if (product.InventoryCount >= quantity)
                        {
                            cart.Add(new OrderDetailsModel
                            {
                                Quantity = quantity,
                                ProductId = productId,
                                ProductPrice = product.ProductPrice,
                                ProductCategory = product.ProductCategory,
                                OrderDate = DateTime.Now,
                                ReturnDate = DateTime.Now.AddMonths(6),
                                Returned = false,
                                UID = await GetCurrentUserId(),
                                BikesPartsImage = product.RentalBikeImage
                            });
                            product.InventoryCount -= quantity;

                            await _context.SaveChangesAsync();

                        }

                    }

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "rentcart", cart);
                    return RedirectToAction("FinalCart");
                }

            }
            
            
        }




        [Authorize]
        public async Task<IActionResult> Checkout()
        {

            if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart") == null)
            {
                return RedirectToAction("Rent", "Index");
            }
            else
            {
                var rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
                if(rentcart.Count==1 && rentcart[0].Quantity==1)
                {
                    CartModel finalcart = new CartModel();
                    finalcart.UID = await GetCurrentUserId();
                    finalcart.OrderId = Guid.NewGuid();
                    finalcart.OrderDate = DateTime.UtcNow;
                    finalcart.SubTotal = rentcart.Sum(item => item.TotalPrice);
                    finalcart.ReturnDate = DateTime.Now.AddMonths(6);
                    finalcart.OrderDetails = rentcart;

                    _context.CartModel.Add(finalcart);
                    await _context.SaveChangesAsync();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "rentcart", null);
                    string message = $"Order placed for rent. Order ID - {finalcart.OrderId}.";
                    _logger.LogInformation(message);

                    return RedirectToAction("Success");
                }
                else
                {
                    for(int i=0; i<rentcart.Count; i++)
					{
                        var product = GetProductById(rentcart[i].ProductId);
                        product.InventoryCount += rentcart[i].Quantity;
                        await _context.SaveChangesAsync();
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "rentcart", null);
                    return View("NoRent");
                }
                
            }
            

        }

        [Authorize]
        public IActionResult Success()
        {
            return View();
        }

      


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(Guid productId)
        {
            List<OrderDetailsModel> rentcart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "rentcart");
            int index = isExist(productId);
            var product = GetProductById(productId);
            product.InventoryCount += rentcart[index].Quantity;
            rentcart.RemoveAt(index);
            await _context.SaveChangesAsync();
            
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
