using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.Commands.ResumeDraft;
using ResumeManager.DataAccess.Enums;
using ResumeManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManager.Services
{
    public class ResumeDraftApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public ResumeDraftApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeApplicationService>();
        }

        public async Task<ResumeDraft> GetResumeDraftById(int resumeDraftId)
        {
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftLanguages)
                .Include(rd => rd.ResumeDraftSkills)
                .Include(rd => rd.DraftQualifications)
                .FirstOrDefaultAsync(rd => rd.Id == resumeDraftId);
            if (resumeDraft == null)
            {
                _logger.LogError(0, new InvalidOperationException(), "No resumeDrafts found in the database for selected user.");
            }
            return resumeDraft;
        }

        public async Task<List<int>> GetLanguageIds(int resumeDraftId)
        {
            var languageNames = _context.ResumeDraftLanguages.Where(x => x.ResumeDraftId == resumeDraftId).Select(x => x.LanguageName);
            var languageIds = await _context.Languages.Where(l => languageNames.Contains(l.Name)).Select(c => c.Id).ToListAsync();
            return languageIds;
        }

        public async Task<ResumeDraft> GetResumeDraftByUserId(int userId)
        {
            var resumeDraft = await _context.ResumeDrafts.Include(r => r.ResumeDraftLanguages).FirstOrDefaultAsync(r => r.UserId == userId);
            if (resumeDraft == null)
            {
                _logger.LogError(0, new InvalidOperationException(), "No resumeDrafts found in the database for selected user.");
            }
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
            }
            resumeDraft.FirstName = command.FirstName;
            resumeDraft.LastName = command.LastName;
            resumeDraft.Email = command.Email;
            resumeDraft.Address = command.Address;
            resumeDraft.DateModified = DateTime.Now;
            resumeDraft.GitHub = command.GitHub;
            resumeDraft.Interests = command.Interests;
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
                        .Select(t => new ResumeDraftLanguage { LanguageName = t.Name }).ToList();
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

        public async Task<List<string>> GetSkills(int resumeDraftId)
        {
            var resumeDraft = await GetResumeDraftById(resumeDraftId);
            var skillsList = _context.ResumeDraftSkills
                .Where(rs => rs.ResumeDraftId == resumeDraft.Id)
                .OrderBy(s => s.SkillName)
                .Select(rs => rs.SkillName);
            return skillsList.ToList();
        }

        public List<string> GetPreloadedSkills()
        {
            return _context.Skills.OrderBy(s => s.Name).Select(s => s.Name).ToList();
        }

        public async Task AddSkill(int resumeDraftId, string skill)
        {
            if (await CheckForExistingSkill(resumeDraftId, skill))
            {
                throw new InvalidOperationException("Skill has been assigned already");
            }
            var resumeDraftSkill = new ResumeDraftSkill
            {
                ResumeDraftId = resumeDraftId,
                SkillName = skill
            };
            await _context.ResumeDraftSkills.AddAsync(resumeDraftSkill);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSkill(int resumeDraftId, string skill)
        {
            var resumeDraftSkill = await _context.ResumeDraftSkills.FirstOrDefaultAsync(rds => rds.SkillName.Equals(skill, StringComparison.OrdinalIgnoreCase));
            _context.ResumeDraftSkills.Remove(resumeDraftSkill);
            await _context.SaveChangesAsync();
        }
        private async Task<bool> CheckForExistingSkill(int resumeDraftId, string skill)
        {
            return await _context.ResumeDraftSkills
                 .AnyAsync(rds => rds.ResumeDraftId == resumeDraftId && rds.SkillName.Equals(skill, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddQualification(AddQualificationCommand command)
        {
            var draftQualification = new DraftQualification
            {
                DateAquired = command.DateAquired,
                InstitutionName = command.Institution,
                Name = command.Name,
                Type = command.Type,
                OtherInformation = command.OtherInfo,
                ResumeDraftId = command.ResumeDraftId
            };
            await _context.DraftQualifications.AddAsync(draftQualification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DraftQualification>> GetQualifications(int resumeDraftId)
        {
            var resumeDraft = await GetResumeDraftById(resumeDraftId);
            var qualificationList = _context.DraftQualifications
                .Where(rs => rs.ResumeDraftId == resumeDraft.Id).OrderByDescending(q => q.DateAquired).ToList();

            return qualificationList;
        }

        public async Task RemoveQualification(int draftQualificationId)
        {
            var draftQual = await _context.DraftQualifications.FirstOrDefaultAsync(dq => dq.Id == draftQualificationId);
            _context.DraftQualifications.Remove(draftQual);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQualification(UpdateQualificationCommand command)
        {
            var draftQualification = await GetQualification(command.DraftQualId);
            draftQualification.DateAquired = command.DateAquired;
            draftQualification.InstitutionName = command.Institution;
            draftQualification.Name = command.Name;
            draftQualification.Type = command.Type;
            draftQualification.OtherInformation = command.OtherInfo;
            await _context.SaveChangesAsync();
        }


        public async Task<DraftQualification> GetQualification(int draftQualId)
        {
            var qual = await _context.DraftQualifications.FirstOrDefaultAsync(dq => dq.Id == draftQualId);
            return qual;
        }
    }
}
