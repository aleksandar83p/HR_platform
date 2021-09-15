using HR_platform.Models;
using HR_platform.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidatesController : ControllerBase
    {
        private readonly IJobCandidateRepository _jobCandidateRepository;
        public JobCandidatesController(IJobCandidateRepository jobCandidateRepository)
        {
            this._jobCandidateRepository = jobCandidateRepository;
        }

        [HttpGet]
        // GET http://localhost:20616/api/JobCandidates
        public async Task<ActionResult> GetJobCandidatesAsync()
        {
            try
            {
                return Ok(await _jobCandidateRepository.GetJobCandidatesAsync());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }            
        }

        [HttpGet("{id:int}")]
        // GET http://localhost:20616/api/JobCandidates/1
        public async Task<ActionResult<JobCandidate>> GetJobCandidateAsync(int id)
        {
            try
            {
                var result = await _jobCandidateRepository.GetJobCandidateByIdAsync(id);

                if(result == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        // POST http://localhost:20616/api/JobCandidates
        public async Task<ActionResult<JobCandidate>> CreateJobCandidate(JobCandidate jobCandidate)
        {
            try
            {
                if(jobCandidate == null)
                {
                    return BadRequest();
                }

                var createdJobCandidate = await _jobCandidateRepository.AddJobCandidateAsync(jobCandidate);

                return CreatedAtAction(nameof(CreateJobCandidate), new { id = createdJobCandidate.JobCandidateId.ToString()}, createdJobCandidate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new job candidate record");
            }
        }

        [HttpPut("{id:int}")]
        // PUT http://localhost:20616/api/JobCandidates/id
        public async Task<ActionResult<JobCandidate>> UpdateJobCandidateAsync(int id, JobCandidate jobCandidate)
        {
            try
            {
                if (id != jobCandidate.JobCandidateId)
                {
                    return BadRequest("Job candidate ID mismatch");
                }

                var jobCandidateToUpdate = await _jobCandidateRepository.GetJobCandidateByIdAsync(id);

                if(jobCandidateToUpdate == null)
                {
                    return NotFound($"Job candidate with ID = {id} not found.");
                }

                return await _jobCandidateRepository.UpdateJobCandidateAsync(jobCandidate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating new job candidate record");
            }
        }

        [HttpDelete("{id:int}")]
        // DELETE http://localhost:20616/api/JobCandidates/id
        public async Task<ActionResult> DeleteJobCandidateAsync(int id)
        {
            try
            {
                var jobCandidateToDelete = await _jobCandidateRepository.GetJobCandidateByIdAsync(id);

                if (jobCandidateToDelete == null)
                {
                    return NotFound($"Job candidate with ID = {id} not found.");
                }

                await _jobCandidateRepository.DeleteJobCandidateAsync(id);

                return Ok($"Job candidate with ID = {id} deleted.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting job candidate record");
            }
        }

        [HttpGet("{search}")]
        // GET  http://localhost:20616/api/JobCandidates/search?name=leo
        public async Task<ActionResult<IEnumerable<JobCandidate>>> SearchAsync(string name)
        {
            try
            {
                var result = await _jobCandidateRepository.SearchAsync(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
