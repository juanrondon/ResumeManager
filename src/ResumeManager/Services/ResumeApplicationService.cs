using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    }
}
