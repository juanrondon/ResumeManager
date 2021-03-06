﻿using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class ResumeLanguage
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
        [Required]
        public string LanguageName { get; set; }
    }
}
