using Microsoft.Extensions.Logging;
using ResumeManager.DataAccess.Models;
using System.Threading.Tasks;

namespace ResumeManager.Services
{
    public class UserApplicationService
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ILogger _logger;

        public UserApplicationService(ResumeManagerDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<UserApplicationService>();
        }

        //Registers a new user in the local db
        public async Task<User> RegisterUser(string email)
        {
            var user = new User
            {
                Email = email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
