using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter {0} here")]
        [DisplayName("User name")]
        [StringLength(20,MinimumLength = 3, ErrorMessage = "User name must be between 3 and 20 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Enter your {0} here")]
        [DisplayName("Password")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password must be at least 7 characters long.")]
        public required string Password { get; set; }
    }
}
