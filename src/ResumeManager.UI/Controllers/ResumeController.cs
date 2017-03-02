using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var model = new ResumeCreateViewModel
            {
                LanguageList = new MultiSelectList(_context.Languages.ToList(), "LanguageId", "Name")
            };
            return View(model);
        }


        [HttpGet]
        public IActionResult GetLanguages()
        {
            var result = _context.Languages.OrderBy(l => l.Name).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public void SaveContactDetails(ResumeCreateViewModel model)
        {
            
        }


        [HttpGet]
        public string SaveCoreSkills(ResumeCreateViewModel model)
        {
            //model.LanguageList = new MultiSelectList(_context.Languages.ToList(), "LanguageId", "Name", selectedValues);
            return "Saved";
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