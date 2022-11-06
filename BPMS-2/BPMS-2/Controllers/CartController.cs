﻿using BPMS_2.Data;
using BPMS_2.Models;
using LearnASPNETCoreMVCWithRealApps.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPMS_2.Controllers
{
    public class CartController : Controller
    {

        private readonly BPMS_2Context _context;
        public CartController(BPMS_2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductsModel.ToListAsync());
        }
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



        [HttpPost("{productId}")]
        public async Task<IActionResult> AddOrder(Guid productId, int quantity)
        {
            var product = GetProductById(productId);
            if (SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart") == null)
            {
                List<OrderDetailsModel> cart = new List<OrderDetailsModel>();
                if(quantity<product.InventoryCount)
                {
                    cart.Add(new OrderDetailsModel
                    {

                        Quantity = quantity,
                        ProductId = productId,
                        ProductPrice = product.ProductPrice
                    });
                    product.InventoryCount -= quantity;
                    
                    await _context.SaveChangesAsync();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                }
                
            }

            else
            {
                List<OrderDetailsModel> cart = SessionHelper.GetObjectFromJson<List<OrderDetailsModel>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1 && product.InventoryCount > quantity)
                {
                    cart[index].Quantity+=quantity;
                    product.InventoryCount -= quantity;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    if(product.InventoryCount>quantity)
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


        

        [HttpPost]
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