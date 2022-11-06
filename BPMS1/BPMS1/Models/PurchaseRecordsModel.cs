using System.ComponentModel.DataAnnotations;

namespace BPMS1.Models
{
    public class PurchaseRecordsModel
    {
        [Key]
        public Guid OrderId { get; set; }
        
        [Required]
        public string UID { get; set; }
        
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseAmount { get; set; }

        
    }
}
