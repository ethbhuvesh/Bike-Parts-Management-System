using System.ComponentModel.DataAnnotations;

namespace ScaffoldingCheckApp.Models
{
    public class PartsAndAccessoriesInventory
    {
        [Key]
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public int ItemPrice { get; set; }

    }
}
