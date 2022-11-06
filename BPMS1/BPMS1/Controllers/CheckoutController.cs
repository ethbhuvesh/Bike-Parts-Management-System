/*

using BPMS1.Data;
using BPMS1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BPMS1.Controllers
{
    public class CheckoutController : Controller
    {
        OrderDetailsModel orderDetails;
        private readonly BPMS1Context _context;
        Cart cart;
        
        public Guid CreateOrder(PurchaseRecordsModel order)
        {
            //order.OrderId=Guid.NewGuid();
            _context.PurchaseRecordsModel.Add(order);
            _context.SaveChanges();
            return order.OrderId;

        }

        [HttpPost]
        public IActionResult Checkout(PurchaseRecordsModel model)
        {
            model.UID = 11443332.ToString();
            
            model.OrderId = CreateOrder(model);
            return View("Index");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}

*/
