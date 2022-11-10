using BPMS_2.Utils;
using System.ComponentModel.DataAnnotations;

namespace BPMS_2.ViewModels
{
    public class RegisterViewModel
    {
        
        public string Username { get; set; } 

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [ValidEmailDomain(allowedDomain: "umd.edu", ErrorMessage = "Only UMD students are allowed. Use your umd.edu account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
