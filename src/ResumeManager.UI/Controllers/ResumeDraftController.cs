using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.DataAccess.Models;
using ResumeManager.Services;
using ResumeManager.DataAccess.Enums;
using ResumeManager.UI.Models.ResumeDraft;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.Commands.ResumeDraft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using ResumeManager.UI.Models.DraftQualification;

namespace ResumeManager.UI.Controllers
{
    [Authorize]
    public class ResumeDraftController : Controller
    {
        private readonly ResumeManagerDbContext _context;
        private readonly ResumeDraftApplicationService _resumeDraftService;

        public ResumeDraftController(ResumeManagerDbContext context, ResumeDraftApplicationService resumeDraftService)
        {
            _context = context;
            _resumeDraftService = resumeDraftService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var results = _context.ResumeDrafts.Where(rd => rd.Status == ResumeDraftStatus.Draft.ToString())
                .OrderByDescending(rd => rd.DateModified).Select(rd => new ResumeDraftIndexViewModel
                {
                    Id = rd.Id,
                    FullName = "Resume - " + rd.FirstName + " " + rd.LastName,
                    LastModified = rd.DateModified
                }).ToList();
            return View(results);
        }

        //Create initiates the draft and in the post we will update the Resume draft
        [HttpGet]
        public async Task<IActionResult> Create(int? resumeDraftId)
        {
            //existing ResumeDraft
            if (resumeDraftId != null)
            {
                var existingResumeDraft = await _resumeDraftService.GetResumeDraftById(resumeDraftId.Value);

                var draft = new ResumeDraftCreateViewModel
                {
                    Id = existingResumeDraft.Id,
                    UserId = existingResumeDraft.UserId,
                    Address = existingResumeDraft.Address,
                    FirstName = existingResumeDraft.FirstName,
                    Email = existingResumeDraft.Email,
                    GitHub = existingResumeDraft.GitHub,
                    Interests = existingResumeDraft.Interests,
                    LastName = existingResumeDraft.LastName,
                    LinkedIn = existingResumeDraft.LinkedIn,
                    Mobile = existingResumeDraft.Mobile,
                    PersonalSkills = existingResumeDraft.PersonalSkills,
                    LanguageList = new MultiSelectList(_context.Languages.OrderBy(l => l.Name).ToList(), "Id", "Name", await _resumeDraftService.GetLanguageIds(existingResumeDraft.Id)),
                    References = existingResumeDraft.References
                };
                if (existingResumeDraft.Photo != null)
                {
                    var imageBase64 = Convert.ToBase64String(existingResumeDraft.Photo);
                    draft.ProfilePhotoBase64 = string.Format("data:{0};base64,{1}", existingResumeDraft.PhotoFileType, imageBase64);
                }
                return View(draft);
            }
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var userId = _context.Users.FirstOrDefault(us => us.Email == email).Id;
            var resumeDraft = await _resumeDraftService.InitiateResumeDraft(userId);
            var model = new ResumeDraftCreateViewModel
            {
                Id = resumeDraft.Id,
                LanguageList = new MultiSelectList(_context.Languages.OrderBy(l => l.Name).ToList(), "Id", "Name"),
                UserId = resumeDraft.UserId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResumeDraftCreateViewModel model, IFormFile profilePhoto)
        {            
            var command = new UpdateResumeDraftCommand
            {
                Address = model.Address,
                Email = model.Email,
                FirstName = model.FirstName,
                GitHub = model.GitHub,
                Id = model.Id,
                Interests = model.Interests,
                LastName = model.LastName,
                LinkedIn = model.LinkedIn,
                Mobile = model.Mobile,
                PersonalSkills = model.PersonalSkills,
                References = model.References,
                ResumeDraftLanguagesIds = model.LanguageListIds,
                UserId = model.UserId
            };
            if (profilePhoto != null && profilePhoto.Length > 0)
            {
                using (var fileStream = profilePhoto.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    command.ProfilePhoto = fileBytes;
                    command.ProfilePhotoFileType = profilePhoto.ContentType;
                }
            }
            await _resumeDraftService.UpdateResumeDraft(command);
            return RedirectToAction("Index");          
        }

        public async Task<IActionResult> Delete(int resumeDraftId)
        {
            await _resumeDraftService.DeleteResumeDraft(resumeDraftId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task RemovePhoto(int resumeDraftId)
        {
            await _resumeDraftService.DeleteProfilePhoto(resumeDraftId);
        }

        [HttpGet]
        public async Task<IActionResult> GetSkills(int resumeDraftId)
        {
            var list = await _resumeDraftService.GetSkills(resumeDraftId);
            var objectList = list.Select(q => new { name = q.SkillName, id = q.Id}).ToList();
            return Ok(objectList);
        }

        [HttpPost]
        public async Task RemoveSkill(int skillId)
        {
            await _resumeDraftService.RemoveSkill(skillId);
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
                await _resumeDraftService.AddSkill(resumeDraftId, skill);
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
            return Ok(_resumeDraftService.GetPreloadedSkills());
        }

        [HttpPost]
        public async Task<IActionResult> AddQualification(AddQualificationViewModel model)
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
            var command = new AddQualificationCommand
            {
                Name = model.Name,
                DateAquired = model.DateAquired,
                Institution = model.Institution,
                OtherInfo = model.OtherInfo,
                ResumeDraftId = model.ResumeDraftId,
                Type = model.Type
            };
            await _resumeDraftService.AddQualification(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDraftQualification(UpdateQualificationViewModel model)
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
            var command = new UpdateQualificationCommand
            {
                DraftQualId = model.DraftQualificationId,
                Name = model.Name,
                DateAquired = model.DateAquired,
                Institution = model.Institution,
                OtherInfo = model.OtherInfo,
                Type = model.Type
            };
            await _resumeDraftService.UpdateQualification(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetQualifications(int resumeDraftId)
        {
            var list = await _resumeDraftService.GetQualifications(resumeDraftId);
            var objectList = list.Select(q => new { name = q.Name, type = q.Type, institution = q.InstitutionName, dateAquired = q.DateAquired, otherInfo = q.OtherInformation, id = q.Id }).ToList();
            return Ok(objectList);
        }

        [HttpPost]
        public async Task RemoveQualification(int draftQualificationId)
        {
            await _resumeDraftService.RemoveQualification(draftQualificationId);
        }

        [HttpGet]
        public async Task<IActionResult> GetDraftQualification(int draftQualificationId)
        {
            var qualification = await _resumeDraftService.GetQualification(draftQualificationId);
            var qual = new
            {
                name = qualification.Name,
                type = qualification.Type,
                institution = qualification.InstitutionName,
                dateAquired = qualification.DateAquired,
                otherInfo = qualification.OtherInformation,
                id = qualification.Id
            };
            return Ok(qual);
        }
    }
}