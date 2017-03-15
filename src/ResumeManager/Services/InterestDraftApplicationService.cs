using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumeManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Services
{
    public class InterestDraftApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public InterestDraftApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ResumeDraftApplicationService>();
        }

        public List<string> GetPreloadedInterests()
        {
            return _context.Interests.OrderBy(s => s.Name).Select(s => s.Name).ToList();
        }

        public async Task AddInterest(int resumeDraftId, string interest)
        {
            if (await CheckForExistingInterest(resumeDraftId, interest))
                throw new InvalidOperationException("Interest has been assigned already.");
            var resumeDraftInterest = new ResumeDraftInterest
            {
                ResumeDraftId = resumeDraftId,
                InterestName = interest
            };
            await _context.ResumeDraftInterests.AddAsync(resumeDraftInterest);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveInterest(int interestId)
        {
            var resumeDraftInterest = await _context.ResumeDraftInterests.FirstOrDefaultAsync(rdi => rdi.Id == interestId);
            _context.ResumeDraftInterests.Remove(resumeDraftInterest);
            await _context.SaveChangesAsync();
        }
        private async Task<bool> CheckForExistingInterest(int resumeDraftId, string interest)
        {
            return await _context.ResumeDraftInterests
                .AnyAsync(rdi => rdi.ResumeDraftId == resumeDraftId &&
                                 rdi.InterestName.Equals(interest, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<List<ResumeDraftInterest>> GetInterests(int resumeDraftId)
        {
            var resumeDraft = await _context.ResumeDrafts
                .Include(rd => rd.ResumeDraftInterests)
                .FirstOrDefaultAsync(rd => rd.Id == resumeDraftId);
            var interestsList = _context.ResumeDraftInterests
                .Where(rs => rs.ResumeDraftId == resumeDraft.Id)
                .OrderBy(i => i.InterestName)
                .ToList();
            return interestsList;
        }
    }
}
