using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HR_platform.Models
{
    public class JobCandidate
    {
        public int JobCandidateId { get; set; }
        [Required]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }        
    }
}
