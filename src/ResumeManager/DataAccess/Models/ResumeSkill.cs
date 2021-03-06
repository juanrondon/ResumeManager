﻿using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeSkill
    {
        public int Id { get; set; }
        [Required]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
        [Required]
        public string SkillName { get; set; }
    }
}
