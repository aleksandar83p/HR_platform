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
    public class JobCandidateSkillsController : ControllerBase
    {
        private readonly IJobCandidateSkillsRepository _jobCandidateSkillsRepository;

        public JobCandidateSkillsController(IJobCandidateSkillsRepository jobCandidateSkillsRepository)
        {
            this._jobCandidateSkillsRepository = jobCandidateSkillsRepository;
        }

        [HttpGet]
        // GET http://localhost:20616/api/JobCandidateSkills
        public async Task<ActionResult> GetJobCandidatesSkillsAsync()
        {
            try
            {
                return Ok(await _jobCandidateSkillsRepository.GetJobCandidateSkillsAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        // GET http://localhost:20616/api/JobCandidateSkill/1
        public async Task<ActionResult<JobCandidateSkill>> GetJobCandidateSkillByIdAsync(int id)
        {
            try
            {
                var result = await _jobCandidateSkillsRepository.GetJobCandidateSkillByIdAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        // POST http://localhost:20616/api/JobCandidateSkills
        public async Task<ActionResult<JobCandidateSkill>> CreateJobCandidateSkill(JobCandidateSkill jobCandidateSkills)
        {
            try
            {
                if (jobCandidateSkills == null)
                {
                    return BadRequest();
                }

                var createdJobCandidateSkill = await _jobCandidateSkillsRepository.AddJobCandidateSkillAsync(jobCandidateSkills);

                return CreatedAtAction(nameof(CreateJobCandidateSkill), new { id = createdJobCandidateSkill.Id.ToString() }, createdJobCandidateSkill);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new job candidate skill record");
            }
        }

        [HttpPut("{id:int}")]
        // PUT http://localhost:20616/api/JobCandidateSkills/id
        public async Task<ActionResult<JobCandidateSkill>> UpdateJobCandidateSkillAsync(int id, JobCandidateSkill jobCandidateSkills)
        {
            try
            {
                if (id != jobCandidateSkills.Id)
                {
                    return BadRequest("Job candidate skill ID mismatch");
                }

                var jobCandidateSkillsToUpdate = await _jobCandidateSkillsRepository.GetJobCandidateSkillByIdAsync(id);

                if (jobCandidateSkillsToUpdate == null)
                {
                    return NotFound($"Job candidate with ID = {id} not found.");
                }

                return await _jobCandidateSkillsRepository.UpdateJobCandidateSkillAsync(jobCandidateSkills);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating new job candidate record");
            }
        }

        [HttpDelete("{id:int}")]
        // DELETE http://localhost:20616/api/JobCandidateSkills/id
        public async Task<ActionResult> DeleteJobCandidateSkillAsync(int id)
        {
            try
            {
                var jobCandidateSkillToDelete = await _jobCandidateSkillsRepository.GetJobCandidateSkillByIdAsync(id);

                if (jobCandidateSkillToDelete == null)
                {
                    return NotFound($"Job candidate skill with ID = {id} not found.");
                }

                await _jobCandidateSkillsRepository.DeleteJobCandidateSkillAsync(id);

                return Ok($"Job candidate skill with ID = {id} deleted.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting job candidate skill record");
            }
        }

        [HttpGet("{search}")]
        // GET  http://localhost:20616/api/JobCandidateSkills/search?name=leo
        //      http://localhost:20616/api/JobCandidateSkills/Search?skillName=c#
        //      http://localhost:20616/api/JobCandidateSkills/Search?name=leo&skillName=java
        public async Task<ActionResult<IEnumerable<JobCandidate>>> SearchAsync(string name, string skillName)
        {
            try
            {
                var result = await _jobCandidateSkillsRepository.SearchAsync(name, skillName);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
