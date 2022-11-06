using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BPMS_2.Models
{
   
    public class OrderDetailsModel
    {
        [Key]
        public Guid ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => ProductPrice * Quantity;

    }
}
