using CaseManagement.Web.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Web.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Enter your {0} here")]
        [DisplayName("Full name")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Enter your {0} here")]
        [DisplayName("Email address")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Enter your {0} here")]
        [DisplayName("Phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public required string Contact { get; set; }

        [Required(ErrorMessage = "Enter your {0} here")]
        [DisplayName("Date of birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Select your {0} here")]
        [DisplayName("Gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Enter {0} here")]
        [DisplayName("User name")]
        [StringLength(20,MinimumLength = 3, ErrorMessage = "User name must be between 3 and 20 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Enter your {0} here")]
        [DisplayName("Password")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password must be at least 7 characters long.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Enter your {0} here to confirm")]
        [DisplayName("password")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
       
    }
}
