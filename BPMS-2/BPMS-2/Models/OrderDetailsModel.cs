using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BPMS_2.Models
{
   
    public class OrderDetailsModel
    {
        [Key]
        public int TableId { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Bikes and Parts")]
        public string BikesPartsImage { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCategory { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
        public string UID { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => ProductPrice * Quantity;

    }
}
