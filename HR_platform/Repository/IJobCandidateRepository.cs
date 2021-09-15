using HR_platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Repository
{
    public interface IJobCandidateRepository
    {
        Task<IEnumerable<JobCandidate>> SearchAsync(string name);
        Task<IEnumerable<JobCandidate>> GetJobCandidatesAsync();
        Task<JobCandidate> GetJobCandidateByIdAsync(int jobCandidateId);        
        Task<JobCandidate> AddJobCandidateAsync(JobCandidate jobCandidate);
        Task<JobCandidate> UpdateJobCandidateAsync(JobCandidate jobCandidate);
        Task DeleteJobCandidateAsync(int jobCandidateId);
    }
}
