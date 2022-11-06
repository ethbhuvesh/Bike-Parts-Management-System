using System.ComponentModel.DataAnnotations;

namespace ScaffoldingCheckApp.Models
{
    public class BikesInventory
    {
        [Key]
        public string BikeNumber { get; set; }
        public string ModelName { get; set; }
        public string BikeSize { get; set; }
        public int BikeCount { get; set; }

        public int BikePrice { get; set; }
    }
}
