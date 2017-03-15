using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeManager.DataAccess.Models;
using ResumeManager.Services;
using ResumeManager.UI.Models.ResumeDraft;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.Commands.ResumeDraft;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using ResumeManager.DataAccess.Models.Enums;
using ResumeManager.UI.Models.DraftEducation;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult DraftPreview(int resumeDraftId)
        {
            ViewBag.ResumeDraftId = resumeDraftId;
            return View();
        }

        public async Task<IActionResult> CreatePreview(int resumeDraftId)
        {
            var draft = await _resumeDraftService.GetResumeDraftById(resumeDraftId);
            var model = new ResumeDraftPreviewViewModel
            {
                FullName = draft.FirstName + " " + draft.LastName,
                Email = draft.Email,
                Mobile = draft.Mobile,
                Address = draft.Address,
                GitHub = draft.GitHub,
                LinkedIn = draft.LinkedIn,
                Languages = draft.ResumeDraftLanguages.Select(l => l.LanguageName).ToList(),
                Skills = draft.ResumeDraftSkills.Select(l => l.SkillName).ToList(),
                PersonalSkills = draft.PersonalSkills,
                DraftEducations = draft.ResumeDraftEducations.Select(de => new PreviewEducationViewModel
                {
                    School = de.School,
                    Degree = de.Degree,
                    FieldOfStudy = de.FieldOfStudy,
                    FromYear = de.FromYear,
                    ToYear = de.ToYear,
                    Grade = de.Grade,
                    Description = de.Description
                })
                    .ToList()
            };
            return Ok(model);
        }
    }
}