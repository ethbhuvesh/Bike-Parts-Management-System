using System.ComponentModel.DataAnnotations;

namespace ScaffoldingCheckApp.Models
{
    public class Roles
    {
        [Key]
        public string UID { get; set; }
        public string RoleName { get; set; }

    }
}
