using BPMS_2.Data;
using BPMS_2.Models;
using LearnASPNETCoreMVCWithRealApps.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
    public class CartController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly BPMS_2Context _context;
        public CartController(BPMS_2Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductsModel.ToListAsync());
        }


        public IActionResult Failure()
        {
            return View();
        }

        [Authorize]
        public IActionResult FinalCart()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.TotalPrice);
            //ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }   

        public ProductsModel GetProductById(Guid productId)
        {
            return _context.ProductsModel.SingleOrDefault(p => p.ProductId == productId);
        }



        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.UserName;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddOrder(Guid productId, int quantity)
        {
            var product = GetProductById(productId);
            if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart") == null)
            {
                List<OrderDetailsModel> cart = new List<OrderDetailsModel>();
                if(quantity<=product.InventoryCount)
                {
                    cart.Add(new OrderDetailsModel
                    {

                        Quantity = quantity,
                        ProductId = productId,
                        ProductPrice = product.ProductPrice,
                        ProductCategory = product.ProductCategory,
                        Returned = false,
                        UID=await GetCurrentUserId(),
                        BikesPartsImage=product.BikePartImage


                    });
                    product.InventoryCount -= quantity;
                    
                    await _context.SaveChangesAsync();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    return RedirectToAction("FinalCart");

                }
                else
                {
                    return View("Failure");
                }
                
            }

            else
            {
                List<OrderDetailsModel> cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1 && product.InventoryCount >= quantity)
                {
                    cart[index].Quantity+=quantity;
                    product.InventoryCount -= quantity;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    if(product.InventoryCount>=quantity)
                    {
                        cart.Add(new OrderDetailsModel
                        {
                            Quantity = quantity,
                            ProductId = productId,
                            ProductPrice = product.ProductPrice,
                            ProductCategory=product.ProductCategory,
                            Returned=false,
                            UID = await GetCurrentUserId(),
                            BikesPartsImage = product.BikePartImage

                        });
                        product.InventoryCount -= quantity;

                        await _context.SaveChangesAsync();

                    }
                    
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return RedirectToAction("FinalCart");
            }

            
        }


        

        [HttpPost]
        [Authorize]
        public IActionResult Remove(Guid productId)
        {
            List<OrderDetailsModel> cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart");
            int index = isExist(productId);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("FinalCart");
        }


        private int isExist(Guid productId)
        {
            List<OrderDetailsModel> cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId.Equals(productId))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

        /* 
            cart.OrderDetails.Add(orderDetail);

            _context.OrderDetailsModel.Add(orderDetail);

            await _context.SaveChangesAsync();



            return RedirectToAction("Index1");
            */

       // public async Task<IActionResult> Index1()
        //{
          //  return View();
        //}