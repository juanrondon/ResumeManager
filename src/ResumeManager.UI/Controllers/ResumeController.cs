using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult CreateResume()
        {
            var model = new ResumeCreateViewModel
            {
                LanguageList = new MultiSelectList(_context.Languages.OrderBy(l => l.Name).ToList(), "LanguageId", "Name")
            };
            var resume = _context.Resumes.Include(r => r.ResumeLanguages).FirstOrDefault(r => r.UserId == 1);
            if (resume == null) return View(model);
            model.Address = resume.Address;
            model.Email = resume.Email;
            model.FirstName = resume.FirstName;
            model.Mobile = resume.Mobile;
            model.LastName = resume.LastName;
            model.LanguageListIds = resume.ResumeLanguages.Select(rl => rl.LanguageId).ToList();
            if (resume.Photo != null)
            {
                var imageBase64 = Convert.ToBase64String(resume.Photo);
                model.Photo = string.Format("data:{0};base64,{1}", resume.PhotoFileType, imageBase64);
            }
            else
            {
                model.Photo = "/images/user-default.png";
            }
            return View(model);
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
            _resumeService.SavePhoto(_context.Resumes.FirstOrDefault(r => r.UserId == 1).ResumeId, bytesImage, contentType);
        }

        [HttpPost]
        public void RemovePhoto()
        {
            _resumeService.SavePhoto(_context.Resumes.FirstOrDefault(r => r.UserId == 1).ResumeId, null, null);
        }

        [HttpGet]
        public string SaveCoreSkills(ResumeCreateViewModel model)
        {
            //model.LanguageList = new MultiSelectList(_context.Languages.ToList(), "LanguageId", "Name", selectedValues);
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