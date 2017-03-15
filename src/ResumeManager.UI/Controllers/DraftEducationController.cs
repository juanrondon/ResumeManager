using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.Services;
using ResumeManager.DataAccess.Models;
using ResumeManager.UI.Models.DraftEducation;
using ResumeManager.Commands.DraftEducation;

namespace ResumeManager.UI.Controllers
{
    public class DraftEducationController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly EducationDraftApplicationService _educationDraftService;

        public DraftEducationController(ResumeManagerDbContext context, EducationDraftApplicationService educationDraftService)
        {
            _context = context;
            _educationDraftService = educationDraftService;
        }

        [HttpGet]
        public IActionResult GetPreloadedFieldsOfStudy()
        {
            return Ok(_educationDraftService.GetPreloadedFieldsOfStudy());
        }

        [HttpPost]
        public async Task<IActionResult> AddEducation(AddEducationViewModel model)
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
            var command = new AddEducationCommand
            {
                School = model.School,
                Degree = model.Degree,
                FieldOfStudy = model.FieldOfStudy,
                FromYear = model.FromYear,
                ResumeDraftId = model.ResumeDraftId,
                ToYear = model.ToYear,
                Description = model.Description,
                Grade = model.Grade
            };
            await _educationDraftService.AddEducation(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDraftEducation(UpdateEducationViewModel model)
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
            var command = new UpdateEducationCommand
            {
                DraftEducationId = model.DraftEducationId,
                School = model.School,
                Degree = model.Degree,
                FieldOfStudy = model.FieldOfStudy,
                FromYear = model.FromYear,
                ToYear = model.ToYear,
                Description = model.Description,
                Grade = model.Grade
            };
            await _educationDraftService.UpdateEducation(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetEducations(int resumeDraftId)
        {
            var list = await _educationDraftService.GetEducations(resumeDraftId);
            var objectList = list.Select(e => new
            {
                school = e.School,
                degree = e.Degree,
                fieldOfStudy = e.FieldOfStudy,
                description = e.Description,
                fromYear = e.FromYear,
                toYear = e.ToYear,
                grade = e.Grade,
                id = e.Id
            }).ToList();
            return Ok(objectList);
        }

        [HttpPost]
        public async Task RemoveEducation(int draftEducationId)
        {
            await _educationDraftService.RemoveEducation(draftEducationId);
        }

        [HttpGet]
        public async Task<IActionResult> GetDraftEducation(int draftEducationId)
        {
            var education = await _educationDraftService.GetEducation(draftEducationId);
            var edu = new
            {
                school = education.School,
                degree = education.Degree,
                fieldOfStudy = education.FieldOfStudy,
                description = education.Description,
                fromYear = education.FromYear,
                toYear = education.ToYear,
                grade = education.Grade,
                id = education.Id
            };
            return Ok(edu);
        }
    }
}