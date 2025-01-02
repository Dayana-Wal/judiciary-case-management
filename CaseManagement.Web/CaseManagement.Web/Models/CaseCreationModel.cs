using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Web.Models
{
    public class CaseCreationModel
    {
        [Required(ErrorMessage = "Select {0} ")]
        [Display(Name = "Case Type")]
        public string CaseType { get; set; } = null!;



        [Required(ErrorMessage = "{0} is Required")]
        public string VictimName { get; set; } = null!;



        // public string SelectAdvocate { get; set; } = null!;

        [Required(ErrorMessage = "{0} is Required")]
        public string AccusedName { get; set; } = null!;




        [Required(ErrorMessage = "{0} is Required")]
        public DateTime IncidentDate { get; set; }



        [Required(ErrorMessage = "{0} is Required")]
        public string IncidentLocation { get; set; } = null!;



        [Required(ErrorMessage = "{0} is Required")]
        public List<IFormFile> CaseFiles { get; set; } = new List<IFormFile>();



        [Required(ErrorMessage = "Detailed {0} is Required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description should be minimun of 10 characters")]
        public string Description { get; set; } = null!;



        public List<string> CaseTypes { get; set; } = new List<string>();
        //public List<string> Advocates { get; set; } = new List<string>();


        // Add a Confirm property to handle the checkbox
        [Required(ErrorMessage = "You must confirm the details before submitting.")]
        public bool Confirm { get; set; }
    }
}
