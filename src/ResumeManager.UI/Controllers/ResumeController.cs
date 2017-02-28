using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.DataAccess.Models;
using ResumeManager.Services;
using ResumeManager.UI.Models.Resume;

namespace ResumeManager.UI.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ResumeApplicationService _resumeService;

        public ResumeController(ResumeManagerDbContext context, ResumeApplicationService resumeService)
        {
            _context = context;
            _resumeService = resumeService;
        }

        [HttpGet]
        public IActionResult CreateResume()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateResume(ResumeCreateViewModel model)
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> DisplayResume()
        {
            int userId = 1; //TODO remove hardcoded ID
            var resume = await _resumeService.GetResumeByUserId(userId);
            var mod = new ResumeDetailsViewModel()
            {
                FullName = resume.FirstName + " " + resume.LastName,
                Email = resume.Email,
                Languages = resume.ResumeLanguages,
                Mobile = resume.Mobile,
                Summary = resume.Summary
            };
            return View(mod);
        }
    }
}