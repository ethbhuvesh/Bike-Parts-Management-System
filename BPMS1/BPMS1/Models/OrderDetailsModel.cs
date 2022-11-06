using System.ComponentModel.DataAnnotations;

namespace BPMS1.Models
{
    public class OrderDetailsModel
    {
        [Key]
        public Guid OrderId { get; set; }
        
        public Guid ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => ProductPrice * Quantity;
        public string? UID { get; set; }
        public DateTime? PurchaseDate { get; set; }

        //public BikesModel? Bikes { get; set; }
        //public PartsModel? Parts { get; set; }   


    }
}
