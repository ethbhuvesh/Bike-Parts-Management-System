using System.ComponentModel.DataAnnotations;

namespace Bikes_and_Parts_Management_System.Models
{
    public class PartsAndAccessoriesInventory
    {
        [Key]
        public int ItemNumber { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }

    }
}
