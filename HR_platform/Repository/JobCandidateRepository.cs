using HR_platform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Repository
{
    public class JobCandidateRepository : IJobCandidateRepository, IDisposable
    {
        private AppDbContext _appDbContext;

        public JobCandidateRepository()
        {

        }

        public JobCandidateRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<JobCandidate> AddJobCandidateAsync(JobCandidate jobCandidate)
        {
            var result = await _appDbContext.JobCandidates.AddAsync(jobCandidate);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteJobCandidateAsync(int jobCandidateId)
        {
            var result = await _appDbContext.JobCandidates.FirstOrDefaultAsync(j => j.JobCandidateId == jobCandidateId);

            if(result != null)
            {
                _appDbContext.JobCandidates.Remove(result);
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

        public async Task<JobCandidate> GetJobCandidateByIdAsync(int jobCandidateId)
        {
            return await _appDbContext.JobCandidates.FirstOrDefaultAsync(j => j.JobCandidateId == jobCandidateId);
        }

        public async Task<IEnumerable<JobCandidate>> GetJobCandidatesAsync()
        {
            return await _appDbContext.JobCandidates.ToListAsync();           
        }

        public async Task<IEnumerable<JobCandidate>> SearchAsync(string name)
        {
            IQueryable<JobCandidate> query = _appDbContext.JobCandidates;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(n => n.FullName.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<JobCandidate> UpdateJobCandidateAsync(JobCandidate jobCandidate)
        {
            var result = await _appDbContext.JobCandidates.FirstOrDefaultAsync(j => j.JobCandidateId == jobCandidate.JobCandidateId); 

            if(result != null)
            {
                result.FullName = jobCandidate.FullName;
                result.DateOfBirth = jobCandidate.DateOfBirth;
                result.ContactNumber = jobCandidate.ContactNumber;
                result.Email = jobCandidate.Email;

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
