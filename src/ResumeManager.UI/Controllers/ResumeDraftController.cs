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
            if (ModelState.IsValid)
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
            model.LanguageList = new MultiSelectList(_context.Languages.OrderBy(l => l.Name).ToList(), "Id", "Name");
            return View();
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
            return Ok(list);
        }

        [HttpPost]
        public async Task RemoveSkill(int resumeDraftId, string skill)
        {
            await _resumeDraftService.RemoveSkill(resumeDraftId, skill);
        }

        [HttpGet]
        public async Task<IActionResult> AddSkill(int resumeDraftId, string skill)
        {
            if (skill == null)
            {
                return BadRequest(new { error = "Skill name is required" });
            }
            try
            {
                await _resumeDraftService.AddSkill(resumeDraftId, skill);
            }
            catch (InvalidOperationException e)
            {

                return BadRequest(new { error = e.Message });
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPreloadedSkills()
        {
            return Ok(_resumeDraftService.GetPreloadedSkills());
        }
    }
}