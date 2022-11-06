using System.ComponentModel.DataAnnotations;

namespace ScaffoldingCheckApp.Models
{
    public class PurchaseRecord
    {
        [Key]
        public string UID { get; set; }
        public string PurchaseType  { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? BikeReturnDate { get; set; }
        public string ProductPurchased { get; set; }
        public int TotalAmount { get; set; }


    }
}
