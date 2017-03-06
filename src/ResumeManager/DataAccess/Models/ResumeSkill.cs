using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeSkill
    {
        public int ResumeSkillId { get; set; }
        [Required]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
        [Required]
        public string skillName { get; set; }
    }
}
