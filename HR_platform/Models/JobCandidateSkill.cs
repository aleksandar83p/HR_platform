using System.ComponentModel.DataAnnotations;

namespace HR_platform.Models
{
    public class JobCandidateSkill
    {
        public int Id { get; set; }
        public int JobCandidateId { get; set; }
        public JobCandidate JobCandidate { get; set; }   
        
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
