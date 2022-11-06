using System.ComponentModel.DataAnnotations;

namespace BPMS_2.Models
{
    public class UsersModel
    {
        [Key]
        public string UID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
