using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.Services;
using ResumeManager.DataAccess.Models;
using ResumeManager.UI.Models.DraftExperience;
using ResumeManager.Commands.DraftExperience;

namespace ResumeManager.UI.Controllers
{
    public class DraftExperienceController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ExperienceDraftApplicationService _experienceDraftService;

        public DraftExperienceController(ResumeManagerDbContext context, ExperienceDraftApplicationService experienceDraftService)
        {
            _context = context;
            _experienceDraftService = experienceDraftService;
        }

        [HttpGet]
        public IActionResult GetPreloadedLocations()
        {
            return Ok(_experienceDraftService.GetPreloadedLocations());
        }

        [HttpPost]
        public async Task<IActionResult> AddExperience(AddExperienceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var result = from ms in ModelState
                             where ms.Value.Errors.Any()
                             let fieldKey = ms.Key
                             let errors = ms.Value.Errors
                             from error in errors
                             select new { fieldKey, error.ErrorMessage };
                return BadRequest(result);
            }
            var command = new AddExperienceCommand
            {
                Title = model.Title,
                Company = model.Company,
                CurrentlyWorking = model.CurrentlyWorking,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Location = model.Location,
                ResumeDraftId = model.ResumeDraftId,
                Description = model.Description
            };
            await _experienceDraftService.AddExperience(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDraftExperience(UpdateExperienceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var result = from ms in ModelState
                             where ms.Value.Errors.Any()
                             let fieldKey = ms.Key
                             let errors = ms.Value.Errors
                             from error in errors
                             select new { fieldKey, error.ErrorMessage };
                return BadRequest(result);
            }
            var command = new UpdateExperienceCommand
            {
                DraftExperienceId = model.DraftExperienceId,
                Title = model.Title,
                Company = model.Company,
                CurrentlyWorking = model.CurrentlyWorking,
                EndDate = model.EndDate,
                Location = model.Location,
                Description = model.Description,
            };
            await _experienceDraftService.UpdateExperience(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetExperiences(int resumeDraftId)
        {
            var list = await _experienceDraftService.GetExperiences(resumeDraftId);
            return Ok(list);
        }

        [HttpPost]
        public async Task RemoveExperience(int draftExperienceId)
        {
            await _experienceDraftService.RemoveExperience(draftExperienceId);
        }

        [HttpGet]
        public async Task<IActionResult> GetDraftExperience(int draftExperienceId)
        {
            var experience = await _experienceDraftService.GetExperience(draftExperienceId);
            return Ok(experience);
        }
    }
}