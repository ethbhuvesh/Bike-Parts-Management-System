using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bikes_and_Parts_Management_System.Models
{
    [Keyless]
    public class PurchaseRecord
    {
        
        public int UID { get; set; }
        public string PurchaseType { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int TotalAmount { get; set; }
        public string ProductPurchased { get; set; }

    }
}
