﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }        
        public string InstitutionName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OtherInformation { get; set; }
        [Required]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

    }
}
