using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.DataAccess.Models;
using ResumeManager.Services;
using ResumeManager.UI.Models.Resume;
using Microsoft.AspNetCore.Authorization;

namespace ResumeManager.UI.Controllers
{
    [Authorize]
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
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var userId = _context.Users.FirstOrDefault(us => us.Email == email).Id;

            var model = new ResumeCreateViewModel
            {
                LanguageList = new MultiSelectList(_context.Languages.OrderBy(l => l.Name).ToList(), "LanguageId", "Name")
            };
            var resume = await _resumeService.GetResumeByUserId(userId);
            if (resume == null)
            {
                model.Photo = "/images/user-default.png";
                return View(model);
            }
            //Populating info from existing resume
            model.Address = resume.Address;
            model.ResumeId = resume.Id;
            model.Email = resume.Email;
            model.FirstName = resume.FirstName;
            model.Mobile = resume.Mobile;
            model.LastName = resume.LastName;
            model.LanguageListIds = await _resumeService.GetLanguageIds(resumeId: resume.Id);
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
        public IActionResult GetSkills(int resumeId)
        {

            var resume = _context.Resumes.FirstOrDefault(r => r.Id == resumeId);
            if (resume != null)
            {
                var skillsList = _context.ResumeSkills.Where(rs => rs.ResumeId == resume.Id).OrderBy(s => s.SkillName).Select(rs => rs.SkillName);
                var list = skillsList.Select(s => new { Name = s }).ToList();
                return Ok(list);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllSkills()
        {
            var skillsList = _context.Skills.OrderBy(s => s.Name).Select(s => s.Name);
            return Ok(skillsList);
        }

        [HttpPost]
        public async Task AddSkill(int resumeId, string skill)
        {
            await _resumeService.AddSkill(resumeId: resumeId, skill: skill);
        }

        [HttpPost]
        public async Task RemoveSkill(int resumeId, string skill)
        {
            await _resumeService.RemoveSkill(resumeId: resumeId, skill: skill);
        }

        //[HttpPost]
        //public async Task SaveContactDetails(ResumeCreateViewModel model)
        //{
        //    var command = new CreateResumeDraftCommand
        //    {
        //        LanguageListIds = model.LanguageListIds,
        //        Email = model.Email,
        //        Mobile = model.Mobile,
        //        FirstName = model.FirstName,
        //        GitHub = model.GitHub,
        //        LastName = model.LastName,
        //        LinkedIn = model.LinkedIn,
        //        Address = model.Address
        //    };
        //    var resume = await _resumeService.SaveContactDetails(command);
        //    model.ResumeId = resume.Id;
        //}

        [HttpPost]
        public async Task SavePhoto()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var userId = _context.Users.FirstOrDefault(us => us.Email == email).Id;
            var resumeId = (await _resumeService.GetResumeByUserId(userId)).Id;
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
                resumeId: resumeId,
                photo: bytesImage,
                fileType: contentType);
        }

        [HttpPost]
        public void RemovePhoto(int resumeId)
        {
            _resumeService.SavePhoto(
                resumeId: resumeId,
                photo: null,
                fileType: null);
        }
    }
}