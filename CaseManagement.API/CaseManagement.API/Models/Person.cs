namespace CaseManagement.API.Models
{
    public class Person
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
