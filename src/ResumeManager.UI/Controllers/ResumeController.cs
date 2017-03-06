using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.Commands.Resume;
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
        public async Task<IActionResult> CreateResume()
        {
            var model = new ResumeCreateViewModel
            {
                LanguageList = new MultiSelectList(_context.Languages.OrderBy(l => l.Name).ToList(), "LanguageId", "Name")
            };
            var resume = await _resumeService.GetResumeByUserId(userId: 1);
            if (resume == null)
            {
                model.Photo = "/images/user-default.png";
                return View(model);
            }
            //POpulating info from exisitng resume
            model.Address = resume.Address;
            model.Email = resume.Email;
            model.FirstName = resume.FirstName;
            model.Mobile = resume.Mobile;
            model.LastName = resume.LastName;
            model.LanguageListIds = await _resumeService.GetLanguageIds(resumeId: resume.ResumeId);
            if (resume.Photo != null)
            {
                var imageBase64 = Convert.ToBase64String(resume.Photo);
                model.Photo = string.Format("data:{0};base64,{1}", resume.PhotoFileType, imageBase64);
            }
            else
            {
                model.Photo = "~/images/user-default.png";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult GetSkills()
        {
            var resumeId = _context.Resumes.FirstOrDefault(r => r.UserId == 1).ResumeId;
            var skillsList = _context.ResumeSkills.Where(rs => rs.ResumeId == resumeId).OrderBy(s=>s.skillName).Select(rs=>rs.skillName);
            var list = skillsList.Select(s => new {Name = s}).ToList();
            return Json(list);
        }

        [HttpPost]
        public async Task AddSkill(string skill)
        {
            var resumeId = _context.Resumes.FirstOrDefault(r => r.UserId == 1).ResumeId;
            await _resumeService.AddSkill(resumeId: resumeId, skill: skill);
        }

        [HttpPost]
        public async Task SaveContactDetails(ResumeCreateViewModel model)
        {
            var command = new SaveContactDetailsCommand
            {
                LanguageListIds = model.LanguageListIds,
                Email = model.Email,
                Mobile = model.Mobile,
                FirstName = model.FirstName,
                GitHub = model.GitHub,
                LastName = model.LastName,
                LinkedIn = model.LinkedIn,
                Address = model.Address
            };
            var resume = await _resumeService.SaveContactDetails(command);
            model.ResumeId = resume.ResumeId;
        }

        [HttpPost]
        public void SavePhoto()
        {
            byte[] bytesImage = null;
            string contentType = null;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                var reader = new BinaryReader(file.OpenReadStream());
                bytesImage = reader.ReadBytes((int)file.Length);
                contentType = file.ContentType;
            }
            _resumeService.SavePhoto(
                resumeId: _context.Resumes.FirstOrDefault(r => r.UserId == 1).ResumeId,
                photo: bytesImage,
                fileType: contentType);
        }

        [HttpPost]
        public void RemovePhoto()
        {
            _resumeService.SavePhoto(
                resumeId: _context.Resumes.FirstOrDefault(r => r.UserId == 1).ResumeId,
                photo: null,
                fileType: null);
        }

        [HttpGet]
        public string SaveCoreSkills(ResumeCreateViewModel model)
        {
            //model.LanguageList = new MultiSelectList(_context.Languages.ToList(), "ResumeLanguageId", "Name", selectedValues);
            return "Saved";
        }


        //[HttpGet]
        //public async Task<IActionResult> DisplayResume()
        //{
        //    int userId = 1; //TODO remove hardcoded ID
        //    var resume = await _resumeService.GetResumeByUserId(userId);
        //    var mod = new ResumeDetailsViewModel()
        //    {
        //        FullName = resume.FirstName + " " + resume.LastName,
        //        Email = resume.Email,
        //        Languages = resume.ResumeLanguages,
        //        Mobile = resume.Mobile,
        //        Summary = resume.Summary
        //    };
        //    return View(mod);
        //}
    }
}