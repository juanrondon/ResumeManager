using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ResumeManager.UI.Models.Resume
{
    public class ResumeCreateSkillsViewModel
    {
        [Required]
        public string CoreSkills { get; set; }
    }
}
