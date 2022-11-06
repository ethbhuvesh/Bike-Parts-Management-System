using System.ComponentModel.DataAnnotations;

namespace BPMS1.Models
{
    public class PartsModel
    {
        [Key]
        public Guid ProductId { get; set; }
        
        [Required]
        public string ProductName { get; set; }
        
        [Required]
        public int ProductCount { get; set; }
        
        [Required]
        public decimal ProductPrice { get; set; }

    }
}
