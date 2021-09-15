using HR_platform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Repository
{
    public class JobCandidateSkillsRepository : IJobCandidateSkillsRepository, IDisposable
    {
        private AppDbContext _appDbContext;

        public JobCandidateSkillsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<JobCandidateSkill> AddJobCandidateSkillAsync(JobCandidateSkill jobCandidateSkill)
        {
            var result = await _appDbContext.JobCandidateSkills.AddAsync(jobCandidateSkill);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteJobCandidateSkillAsync(int jobCandidateSkillId)
        {
            var result = await _appDbContext.JobCandidateSkills.FirstOrDefaultAsync(j => j.Id == jobCandidateSkillId);

            if (result != null)
            {
                _appDbContext.JobCandidateSkills.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_appDbContext != null)
                {
                    _appDbContext.Dispose();
                    _appDbContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<JobCandidateSkill> GetJobCandidateSkillByIdAsync(int jobCandidateSkillId)
        {
            return await _appDbContext.JobCandidateSkills
                                      .Include(j => j.JobCandidate)
                                      .Include(s => s.Skill)
                                      .FirstOrDefaultAsync(j => j.Id == jobCandidateSkillId);
        }

        public async Task<IEnumerable<JobCandidateSkill>> GetJobCandidateSkillsAsync()
        {
            return await _appDbContext.JobCandidateSkills
                                      .Include(j => j.JobCandidate)
                                      .Include(s => s.Skill)
                                      .ToListAsync();
        }

        public async Task<IEnumerable<JobCandidateSkill>> SearchAsync(string name, string skillName)
        {
            IQueryable<JobCandidateSkill> query = _appDbContext.JobCandidateSkills
                                                               .Include(j => j.JobCandidate)
                                                               .Include(s => s.Skill);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(n => n.JobCandidate.FullName.Contains(name));                
            }

            if (!string.IsNullOrEmpty(skillName))
            {
                query = query.Where(n => n.Skill.Name.Contains(skillName));
            }

            return await query.ToListAsync();
        }

        public async Task<JobCandidateSkill> UpdateJobCandidateSkillAsync(JobCandidateSkill jobCandidateSkill)
        {
            var result = await _appDbContext.JobCandidateSkills.FirstOrDefaultAsync(j => j.Id == jobCandidateSkill.Id);

            if (result != null)
            {
                result.JobCandidateId = jobCandidateSkill.JobCandidateId;
                result.SkillId = jobCandidateSkill.SkillId;                

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
