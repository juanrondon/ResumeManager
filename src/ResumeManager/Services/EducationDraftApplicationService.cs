using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.Commands.DraftEducation;
using ResumeManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManager.Services
{
    public class EducationDraftApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public EducationDraftApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeDraftApplicationService>();
        }

        public List<string> GetPreloadedFieldsOfStudy()
        {
            return _context.FieldOfStudies.OrderBy(s => s.Name).Select(s => s.Name).ToList();
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
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftEducations)
                .FirstOrDefaultAsync(rd => rd.Id == resumeDraftId);
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
