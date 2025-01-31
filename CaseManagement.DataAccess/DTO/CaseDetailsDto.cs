using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.DataAccess.DTO
{
    public class CaseDetailsDto
    {
        public string CaseNumber { get; set; }
        public string CaseStatus { get; set; }
        public PersonDto Victim { get; set; }
        public PersonDto Accused { get; set; }
        public PersonDto? Advocate { get; set; }
    }

    public class PersonDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Contact { get; set; }
    }
}
