using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.Services;
using ResumeManager.DataAccess.Models;

namespace ResumeManager.UI.Controllers
{
    public class DraftSkillController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly SkillDraftApplicationService _skillDraftService;

        public DraftSkillController(ResumeManagerDbContext context, SkillDraftApplicationService skillDraftService)
        {
            _context = context;
            _skillDraftService = skillDraftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkills(int resumeDraftId)
        {
            var list = await _skillDraftService.GetSkills(resumeDraftId);
            var objectList = list.Select(q => new { name = q.SkillName, id = q.Id }).ToList();
            return Ok(objectList);
        }

        [HttpPost]
        public async Task RemoveSkill(int skillId)
        {
            await _skillDraftService.RemoveSkill(skillId);
        }



        [HttpGet]
        public async Task<IActionResult> AddSkill(int resumeDraftId, string skill)
        {
            if (skill == null)
            {
                ModelState.AddModelError("Skill", "The Skill field is required.");
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
                await _skillDraftService.AddSkill(resumeDraftId, skill);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError("Skill", e.Message);
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
        public IActionResult GetPreloadedSkills()
        {
            return Ok(_skillDraftService.GetPreloadedSkills());
        }
    }
}