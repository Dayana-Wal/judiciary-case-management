using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Web.Models
{
    public class CaseModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Case Type is required.")]
        public int CaseTypeId { get; set; }

        public string CaseType { get; set; } 

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Case Number is required.")]
        [StringLength(50, ErrorMessage = "Case Number cannot exceed 50 characters.")]
        public string CaseNumber { get; set; }

        [Required(ErrorMessage = "Accused is required.")]
        public string AccusedId { get; set; }
        public string Accused { get; set; } 

        [Required(ErrorMessage = "Victim is required.")]
        public string VictimId { get; set; }
        public string Victim { get; set; } 

        public string AdvocateId { get; set; }
        public string Advocate { get; set; } 

        [Required(ErrorMessage = "Case Status is required.")]
        public int CaseStatusId { get; set; }
        public string CaseStatus { get; set; } 

        public List<string> FileIds { get; set; }

        public List<FileModel> Files { get; set; }

        public CaseModel()
        {
            Files = new List<FileModel>();
        }
    }

    public class FileModel
    {
        public string Id { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
