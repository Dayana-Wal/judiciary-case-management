using System.ComponentModel.DataAnnotations;

namespace CaseManagement.API.Models
{
    public class LoginCredentials
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
