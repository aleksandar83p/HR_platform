using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_platform.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        [Required]
        public string Name { get; set; }        
    }
}
