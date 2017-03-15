using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Services
{
    public class SkillDraftApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public SkillDraftApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeDraftApplicationService>();
        }

        public async Task<List<ResumeDraftSkill>> GetSkills(int resumeDraftId)
        {
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftSkills)
                .FirstOrDefaultAsync(rd => rd.Id == resumeDraftId);
            var skillsList = _context.ResumeDraftSkills
                .Where(rs => rs.ResumeDraftId == resumeDraft.Id)
                .OrderBy(s => s.SkillName)
                .ToList();

            return skillsList;
        }

        public List<string> GetPreloadedSkills()
        {
            return _context.Skills.OrderBy(s => s.Name).Select(s => s.Name).ToList();
        }

        public async Task AddSkill(int resumeDraftId, string skill)
        {
            if (await CheckForExistingSkill(resumeDraftId, skill))
                throw new InvalidOperationException("Skill has been assigned already.");
            var resumeDraftSkill = new ResumeDraftSkill
            {
                ResumeDraftId = resumeDraftId,
                SkillName = skill
            };
            await _context.ResumeDraftSkills.AddAsync(resumeDraftSkill);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSkill(int skillId)
        {
            var resumeDraftSkill = await _context.ResumeDraftSkills.FirstOrDefaultAsync(rds => rds.Id == skillId);
            _context.ResumeDraftSkills.Remove(resumeDraftSkill);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CheckForExistingSkill(int resumeDraftId, string skill)
        {
            return await _context.ResumeDraftSkills
                .AnyAsync(rds => rds.ResumeDraftId == resumeDraftId &&
                                 rds.SkillName.Equals(skill, StringComparison.OrdinalIgnoreCase));
        }
    }
}
