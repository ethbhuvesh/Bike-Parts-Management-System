using Microsoft.EntityFrameworkCore;

namespace BPMS1.Models
{
    [Keyless]
    public class Cart
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public string UID { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }

        public decimal SubTotal =>
            OrderDetails.Sum(od => od.TotalPrice);

        public Cart()
        {
            OrderDetails = new List<OrderDetailsModel>();
        }
    }
}
