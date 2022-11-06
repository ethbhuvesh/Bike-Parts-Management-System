using System.ComponentModel.DataAnnotations;

namespace Bikes_and_Parts_Management_System.Models
{
    public class Users
    {
        [Key]
        public int UID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string UserRole { get; set; }
        public string PasswordHash { get; set; }

    }
}
