using HR_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Repository
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> SearchAsync(string skill);
        Task<IEnumerable<Skill>> GetSkillsAsync();
        Task<Skill> GetSkillByIdAsync(int skillId);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<Skill> UpdateSkillAsync(Skill skill);
        Task<Skill> DeleteSkillAsync(int skillId);
    }
}
