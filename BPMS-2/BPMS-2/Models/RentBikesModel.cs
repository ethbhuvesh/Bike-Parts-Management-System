using System.ComponentModel.DataAnnotations;

namespace BPMS_2.Models
{
    public class RentBikesModel
    {
        [Key]
        public Guid ProductId { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Rental Bike")]
        public string RentalBikeImage { get; set; }
        public string ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public int ProductSize { get; set; }
        public int InventoryCount { get; set; }
        public decimal ProductPrice { get; set; }

    }
}  