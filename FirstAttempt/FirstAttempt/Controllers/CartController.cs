using FirstAttempt.Models;
using Microsoft.AspNetCore.Mvc;


namespace FirstAttempt.Controllers
{
    public class CartController : Controller
    {

        public OrderDetails _orderDetails;

        public CartController(OrderDetails orderDetails)
        {
            _orderDetails = orderDetails;
        }


        [HttpPost("{productId}")]
        public IActionResult AddOrder(int productId, short quantity)
        {

            //var product = _orderDetails.bikes.SingleOrDefault(p => p.ProductId == productId);

            var orderDetails = new OrderDetails()
            {
                ProductId = productId,
                Quantity = quantity,
                PurchaseDate = DateTime.Now,
                //UnitPrice = product.BikePrice
            };
            
           
            

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
