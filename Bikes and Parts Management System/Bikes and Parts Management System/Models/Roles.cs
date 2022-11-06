using System.ComponentModel.DataAnnotations;

namespace Bikes_and_Parts_Management_System.Models
{
    public class Roles
    {
        [Key]
        public int UID { get; set; }
        public string Role { get; set; }

    }
}
