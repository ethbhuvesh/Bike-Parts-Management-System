using System.ComponentModel.DataAnnotations;

namespace Bikes_and_Parts_Management_System.Models
{
    public class BikesInventory
    {
        [Key]
        public int BikeNumber { get; set; }
        public string ModelName { get; set; }
        public string Size { get; set; }
        public int BikeCount { get; set; }

    }
}
