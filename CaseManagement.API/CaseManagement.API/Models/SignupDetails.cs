using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.API.Models
{
    public class SignupDetails
    {
   
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
