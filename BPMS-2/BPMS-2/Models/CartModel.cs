using System.ComponentModel.DataAnnotations;

namespace BPMS_2.Models
{
    public class CartModel
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string UID { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime? ReturnDate { get; set; }
        public CartModel()
        {
            OrderDetails= new List<OrderDetailsModel>();
        }
    }
}
