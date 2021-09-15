using HR_platform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_platform.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _appDbContext;
        public SkillRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var result = await _appDbContext.Skills.AddAsync(skill);            
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Skill> DeleteSkillAsync(int skillId)
        {
            var result = await _appDbContext.Skills.FirstOrDefaultAsync(s => s.SkillId == skillId);

            if (result != null)
            {
                _appDbContext.Skills.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Skill> GetSkillByIdAsync(int skillId)
        {
            return await _appDbContext.Skills.FirstOrDefaultAsync(s => s.SkillId == skillId);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _appDbContext.Skills.ToListAsync();
        }

        public async Task<IEnumerable<Skill>> SearchAsync(string skill)
        {
            IQueryable<Skill> query = _appDbContext.Skills;

            if (!string.IsNullOrEmpty(skill))
            {
                query = query.Where(s => s.Name.Contains(skill));
            }

            return await query.ToListAsync();
        }

        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            var result = await _appDbContext.Skills.FirstOrDefaultAsync(s => s.SkillId == skill.SkillId);

            if (result != null)
            {
                result.Name = skill.Name;              

                await _appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
