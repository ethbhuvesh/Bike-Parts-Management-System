using System.ComponentModel.DataAnnotations;

namespace BPMS1.Models
{
    public class UsersModel
    {
        [Key]
        public string UID { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
