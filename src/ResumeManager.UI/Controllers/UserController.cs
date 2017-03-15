using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResumeManager.DataAccess.Models;
using ResumeManager.Services;

namespace ResumeManager.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly UserApplicationService _userService;

        public UserController(ResumeManagerDbContext context, UserApplicationService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> RegisterUser(string email)
        {
            await _userService.RegisterUser(email);

            return RedirectToAction("Index", "ResumeDraft");
        }

        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");
        }
    }
}