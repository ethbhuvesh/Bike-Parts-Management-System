using System.ComponentModel.DataAnnotations;

namespace BPMS_2.Models
{
    public class ProductsModel
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public int ProductSize { get; set; }
        public int InventoryCount { get; set; }
        public decimal ProductPrice { get; set; }


    }
}
