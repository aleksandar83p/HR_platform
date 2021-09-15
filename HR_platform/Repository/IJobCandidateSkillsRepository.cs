using HR_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Repository
{
    public interface IJobCandidateSkillsRepository
    {
        Task<IEnumerable<JobCandidateSkill>> SearchAsync(string name, string skillName);
        Task<IEnumerable<JobCandidateSkill>> GetJobCandidateSkillsAsync();
        Task<JobCandidateSkill> GetJobCandidateSkillByIdAsync(int jobCandidateSkillId);
        Task<JobCandidateSkill> AddJobCandidateSkillAsync(JobCandidateSkill jobCandidateSkill);
        Task<JobCandidateSkill> UpdateJobCandidateSkillAsync(JobCandidateSkill jobCandidateSkill);
        Task DeleteJobCandidateSkillAsync(int jobCandidateSkillId);
    }
}
