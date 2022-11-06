using System.ComponentModel.DataAnnotations;

namespace FirstAttempt.Models
{
    public class Bikes
    {
        [Key]
        public string ProductId { get; set; }
        public string BikeName { get; set; }
        public string BikeSize { get; set; }
        public int BikePrice { get; set; }
        public int BikeCount { get; set; }
    }
}
