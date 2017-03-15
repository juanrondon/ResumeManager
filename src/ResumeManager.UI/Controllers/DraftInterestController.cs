using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.Services;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.UI.Controllers
{
    public class DraftInterestController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly InterestDraftApplicationService _interestDraftService;

        public DraftInterestController(ResumeManagerDbContext context, InterestDraftApplicationService interestDraftService)
        {
            _context = context;
            _interestDraftService = interestDraftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInterests(int resumeDraftId)
        {
            var list = await _interestDraftService.GetInterests(resumeDraftId);
            var objectList = list.Select(q => new { name = q.InterestName, id = q.Id }).ToList();
            return Ok(objectList);
        }

        [HttpPost]
        public async Task RemoveInterest(int interestId)
        {
            await _interestDraftService.RemoveInterest(interestId);
        }

        [HttpGet]
        public async Task<IActionResult> AddInterest(int resumeDraftId, string interest)
        {
            if (interest == null)
            {
                ModelState.AddModelError("Interest", "The Interest field is required.");
                var result = from ms in ModelState
                             where ms.Value.Errors.Any()
                             let fieldKey = ms.Key
                             let errors = ms.Value.Errors
                             from error in errors
                             select new { fieldKey, error.ErrorMessage };
                return BadRequest(result);
            }
            try
            {
                await _interestDraftService.AddInterest(resumeDraftId, interest);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("Interest", e.Message);
                var result = from ms in ModelState
                             where ms.Value.Errors.Any()
                             let fieldKey = ms.Key
                             let errors = ms.Value.Errors
                             from error in errors
                             select new { fieldKey, error.ErrorMessage };
                return BadRequest(result);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPreloadedInterests()
        {
            return Ok(_interestDraftService.GetPreloadedInterests());
        }

    }
}