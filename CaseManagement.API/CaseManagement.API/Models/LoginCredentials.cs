using System.ComponentModel.DataAnnotations;

namespace CaseManagement.API.Models
{
    public class LoginCredentials
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
