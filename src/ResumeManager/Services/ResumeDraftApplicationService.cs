using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.Commands.DraftEducation;
using ResumeManager.Commands.ResumeDraft;
using ResumeManager.DataAccess.Models;
using ResumeManager.DataAccess.Models.Enums;

namespace ResumeManager.Services
{
    public class ResumeDraftApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public ResumeDraftApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeDraftApplicationService>();
        }

        public async Task<ResumeDraft> GetResumeDraftById(int resumeDraftId)
        {
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftLanguages)
                .Include(rd => rd.ResumeDraftSkills)
                .Include(rd => rd.ResumeDraftEducations)
                .FirstOrDefaultAsync(rd => rd.Id == resumeDraftId);
            if (resumeDraft == null)
                _logger.LogError(0, new InvalidOperationException(),
                    "No resumeDrafts found in the database for selected user.");
            return resumeDraft;
        }

        public async Task<List<int>> GetLanguageIds(int resumeDraftId)
        {
            var languageNames = _context.ResumeDraftLanguages.Where(x => x.ResumeDraftId == resumeDraftId)
                .Select(x => x.LanguageName);
            var languageIds = await _context.Languages.Where(l => languageNames.Contains(l.Name))
                .Select(c => c.Id)
                .ToListAsync();
            return languageIds;
        }

        public async Task<ResumeDraft> GetResumeDraftByUserId(int userId)
        {
            var resumeDraft = await _context.ResumeDrafts.Include(r => r.ResumeDraftLanguages)
                .FirstOrDefaultAsync(r => r.UserId == userId);
            if (resumeDraft == null)
                _logger.LogError(0, new InvalidOperationException(),
                    "No resumeDrafts found in the database for selected user.");
            return resumeDraft;
        }


        public async Task<ResumeDraft> InitiateResumeDraft(int userId)
        {
            var resumeDraft = new ResumeDraft
            {
                Status = ResumeDraftStatus.Draft.ToString(),
                DateModified = DateTime.Now,
                UserId = userId
            };
            _context.ResumeDrafts.Add(resumeDraft);
            await _context.SaveChangesAsync();
            return resumeDraft;
        }

        public async Task<ResumeDraft> UpdateResumeDraft(UpdateResumeDraftCommand command)
        {
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftLanguages)
                .Include(rd => rd.ResumeDraftSkills)
                .FirstOrDefaultAsync(rd => rd.Id == command.Id);

            if (resumeDraft == null)
            {
                _logger.LogError(0, "No Resume Draft found for id {0}.", command.Id);
                return null;
            }
            resumeDraft.FirstName = command.FirstName;
            resumeDraft.LastName = command.LastName;
            resumeDraft.Email = command.Email;
            resumeDraft.Address = command.Address;
            resumeDraft.DateModified = DateTime.Now;
            resumeDraft.GitHub = command.GitHub;            
            resumeDraft.LinkedIn = command.LinkedIn;
            resumeDraft.Mobile = command.Mobile;
            resumeDraft.PersonalSkills = command.PersonalSkills;
            //Only update if a new photo is assigned
            if (command.ProfilePhoto != null)
            {
                resumeDraft.Photo = command.ProfilePhoto;
                resumeDraft.PhotoFileType = command.ProfilePhotoFileType;
            }
            resumeDraft.References = command.References;
            // Remove existing ResumeDraftLanguages and add updated ones
            resumeDraft.ResumeDraftLanguages.RemoveAll(t => true);
            if (command.ResumeDraftLanguagesIds != null)
            {
                var languages = _context.Languages.Where(t => command.ResumeDraftLanguagesIds.Contains(t.Id))
                    .Select(t => new ResumeDraftLanguage { LanguageName = t.Name })
                    .ToList();
                if (languages != null)
                    resumeDraft.ResumeDraftLanguages.AddRange(languages);
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Resume Draft {0}, {1} was updated.", resumeDraft.FirstName, resumeDraft.LastName);

            return resumeDraft;
        }

        public async Task DeleteResumeDraft(int resumeDraftId)
        {
            var resumeDraft = await GetResumeDraftById(resumeDraftId);
            _context.ResumeDrafts.Remove(resumeDraft);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfilePhoto(int resumeDraftId)
        {
            var resumeDraft = await GetResumeDraftById(resumeDraftId);
            resumeDraft.Photo = null;
            resumeDraft.PhotoFileType = null;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResumeDraftSkill>> GetSkills(int resumeDraftId)
        {
            var resumeDraft = await GetResumeDraftById(resumeDraftId);
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

        public async Task AddEducation(AddEducationCommand command)
        {
            var draftEducation = new ResumeDraftEducation
            {
                Degree = command.Degree,
                Description = command.Description,
                FieldOfStudy = command.FieldOfStudy,
                Grade = command.Grade,
                FromYear = command.FromYear,
                School = command.School,
                ToYear = command.ToYear,
                ResumeDraftId = command.ResumeDraftId
            };
            await _context.DraftEducations.AddAsync(draftEducation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResumeDraftEducation>> GetEducations(int resumeDraftId)
        {
            var resumeDraft = await GetResumeDraftById(resumeDraftId);
            var educations = _context.DraftEducations
                .Where(rs => rs.ResumeDraftId == resumeDraft.Id)
                .OrderByDescending(q => q.ToYear)
                .ToList();

            return educations;
        }

        public async Task RemoveEducation(int draftEducationId)
        {
            var draftEducation = await _context.DraftEducations.FirstOrDefaultAsync(dq => dq.Id == draftEducationId);
            _context.DraftEducations.Remove(draftEducation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEducation(UpdateEducationCommand command)
        {
            var draftEducation = await GetEducation(command.DraftEducationId);
            draftEducation.Degree = command.Degree;
            draftEducation.ToYear = command.ToYear;
            draftEducation.Description = command.Description;
            draftEducation.FieldOfStudy = command.FieldOfStudy;
            draftEducation.FromYear = command.FromYear;
            draftEducation.Grade = command.Grade;
            draftEducation.School = command.School;
            await _context.SaveChangesAsync();
        }


        public async Task<ResumeDraftEducation> GetEducation(int id)
        {
            var qual = await _context.DraftEducations.FirstOrDefaultAsync(dq => dq.Id == id);
            return qual;
        }
    }
}