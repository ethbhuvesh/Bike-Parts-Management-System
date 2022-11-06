namespace FirstAttempt.Models
{
    public class OrderDetails
    {
        
        public DateTime PurchaseDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }
        public Bikes bikes { get; set; }

        
    }
}
