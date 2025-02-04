namespace CaseManagement.Web.Models
{
    public class ViewCaseModel
    {
        // Case Details
        public string CaseNumber { get; set; }
        public string CaseStatus { get; set; }

        public string CaseType { get; set; }
        public DateTime? DateOfIncident { get; set; }
        public string Description { get; set; }

        // Victim Details
        public VictimDetails Victim { get; set; }

        // Accused Details
        public AccusedDetails Accused { get; set; }

        // Advocate Details (Optional)
        public AdvocateDetails Advocate { get; set; }
    }

    public class VictimDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }

    public class AccusedDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }

    public class AdvocateDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}
