﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string Responsabilities { get; set; }
        public string OtherInformation { get; set; }
        [Required]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
