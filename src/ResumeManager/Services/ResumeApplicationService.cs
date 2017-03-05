using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.Commands.Resume;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.Services
{
    public class ResumeApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public ResumeApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeApplicationService>();
        }

        public async Task<Resume> GetResumeByUserId(int userId)
        {
            var resume = await _context.Resumes.Include(r => r.ResumeLanguages).Where(r => r.UserId == userId).FirstOrDefaultAsync();
            if (resume == null)
            {
                _logger.LogError(0, new InvalidOperationException(), "No resumes found in the database for selected user.");
            }
            return resume;
        }

        public async Task<Resume> SaveContactDetails(SaveContactDetailsCommand command)
        {
            Resume resume;
            if (_context.Resumes.Any(r => r.UserId == 1))
            {
                resume = _context.Resumes.FirstOrDefault(r => r.UserId == 1);
                //Update languages
                var languages = _context.ResumeLanguages.Where(rl => rl.ResumeId == resume.ResumeId);
                //remove existing
                _context.ResumeLanguages.RemoveRange(languages);
                await _context.SaveChangesAsync();
                resume.Address = command.Address;
                resume.Email = command.Email;
                resume.FirstName = command.FirstName;
                resume.GitHub = command.GitHub;
                resume.LastName = command.LastName;
                resume.LinkedIn = command.LinkedIn;
                resume.Mobile = command.Mobile;
            }
            else
            {
                resume = new Resume
                {
                    Address = command.Address,
                    Email = command.Email,
                    FirstName = command.FirstName,
                    GitHub = command.GitHub,
                    LastName = command.LastName,
                    LinkedIn = command.LinkedIn,
                    Mobile = command.Mobile,
                    UserId = 1
                };
                _context.Resumes.Add(resume);
            }
            foreach (var languageId in command.LanguageListIds)
            {
                var resumeLanguage = new ResumeLanguage()
                {
                    LanguageName = _context.Languages.FirstOrDefault(l => l.LanguageId == languageId).Name,
                    Resume = resume
                };
                _context.ResumeLanguages.Add(resumeLanguage);
            }

            await _context.SaveChangesAsync();

            return resume;
        }

        public async Task<List<int>> GetLanguageIds(int resumeId)
        {
            var languageNames = _context.ResumeLanguages.Where(x => x.ResumeId == resumeId).Select(x => x.LanguageName);
            var languageIds = await _context.Languages.Where(l => languageNames.Contains(l.Name)).Select(c => c.LanguageId).ToListAsync();
            return languageIds;
        }

        public void SavePhoto(int resumeId, byte[] photo, string fileType)
        {
            var resume = _context.Resumes.FirstOrDefault(r => r.ResumeId == resumeId);
            resume.Photo = photo;
            resume.PhotoFileType = fileType;
            _context.Update(resume);
            _context.SaveChanges();
        }
    }
}
