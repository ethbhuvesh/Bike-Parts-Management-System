using System.ComponentModel.DataAnnotations;

namespace ScaffoldingCheckApp.Models
{
    public class Users
    {
        [Key]
        public string UID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
