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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository skillRepository)
        {
            this._skillRepository = skillRepository;
        }

        [HttpGet]
        // GET http://localhost:20616/api/Skills
        public async Task<ActionResult> GetSkillsAsync()
        {
            try
            {
                return Ok(await _skillRepository.GetSkillsAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        // GET http://localhost:20616/api/Skills/1
        public async Task<ActionResult<Skill>> GetSkillByIdAsync(int id)
        {
            try
            {
                var result = await _skillRepository.GetSkillByIdAsync(id);

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
        // POST http://localhost:20616/api/Skills
        public async Task<ActionResult<Skill>> CreateSkill(Skill skill)
        {
            try
            {
                if (skill == null)
                {
                    return BadRequest();
                }

                var createdSkill = await _skillRepository.AddSkillAsync(skill);

                return CreatedAtAction(nameof(CreateSkill), new { id = createdSkill.SkillId.ToString() }, createdSkill);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new skill record");
            }
        }

        [HttpPut("{id:int}")]
        // PUT http://localhost:20616/api/Skills/id
        public async Task<ActionResult<Skill>> UpdateSkillAsync(int id, Skill skill)
        {
            try
            {
                if (id != skill.SkillId)
                {
                    return BadRequest("Skill ID mismatch");
                }

                var skillToUpdate = await _skillRepository.GetSkillByIdAsync(id);

                if (skillToUpdate == null)
                {
                    return NotFound($"Skill with ID = {id} not found.");
                }

                return await _skillRepository.UpdateSkillAsync(skill);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating skill record");
            }
        }

        [HttpDelete("{id:int}")]
        // DELETE http://localhost:20616/api/Skills/id
        public async Task<ActionResult> DeleteSkillAsync(int id)
        {
            try
            {
                var skillToDelete = await _skillRepository.GetSkillByIdAsync(id);

                if (skillToDelete == null)
                {
                    return NotFound($"Skill with ID = {id} not found.");
                }

                await _skillRepository.DeleteSkillAsync(id);

                return Ok($"Skill with ID = {id} deleted.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting skill record");
            }
        }

        [HttpGet("{search}")]
        // GET  http://localhost:20616/api/Skills/search?name=prog
        public async Task<ActionResult<IEnumerable<Skill>>> SearchAsync(string name)
        {
            try
            {
                var result = await _skillRepository.SearchAsync(name);

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
