using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.Commands.DraftExperience;
using ResumeManager.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Services
{
    public class ExperienceDraftApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public ExperienceDraftApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeDraftApplicationService>();
        }

        public List<string> GetPreloadedLocations()
        {
            return _context.Locations.OrderBy(s => s.Name).Select(s => s.Name).ToList();
        }

        public async Task AddExperience(AddExperienceCommand command)
        {
            var draftExperience = new ResumeDraftExperience
            {
                Title=command.Title,
                Description = command.Description,
                Company=command.Company,
                Location=command.Location,
                CurrentlyWorking=command.CurrentlyWorking,
                EndDate=command.EndDate,
                StartDate=command.StartDate,
                ResumeDraftId = command.ResumeDraftId
            };
            await _context.DraftExperiences.AddAsync(draftExperience);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResumeDraftExperience>> GetExperiences(int resumeDraftId)
        {
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftExperiences)
                .FirstOrDefaultAsync(rd => rd.Id == resumeDraftId);
            var experiences = _context.DraftExperiences
                .Where(rs => rs.ResumeDraftId == resumeDraft.Id)
                .OrderByDescending(q => q.EndDate)
                .ToList();
            return experiences;
        }

        public async Task RemoveExperience(int draftExperienceId)
        {
            var draftExperience = await _context.DraftExperiences.FirstOrDefaultAsync(de => de.Id == draftExperienceId);
            _context.DraftExperiences.Remove(draftExperience);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExperience(UpdateExperienceCommand command)
        {
            var draftExperience = await GetExperience(command.DraftExperienceId);
            draftExperience.Title = command.Title;
            draftExperience.Company = command.Company;
            draftExperience.Description = command.Description;
            draftExperience.CurrentlyWorking = command.CurrentlyWorking;
            draftExperience.EndDate = command.EndDate;
            draftExperience.StartDate = command.StartDate;
            draftExperience.Location = command.Location;            
            await _context.SaveChangesAsync();
        }

        public async Task<ResumeDraftExperience> GetExperience(int id)
        {
            var draftExperience = await _context.DraftExperiences.FirstOrDefaultAsync(de => de.Id == id);
            return draftExperience;
        }
    }
}
