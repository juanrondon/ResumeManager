using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeManager.UI.Models.ResumeDraft
{
    public class ResumeDraftIndexViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Display(Name = "Last Modified")]
        [DisplayFormat(DataFormatString = "{0:dddd, MMM d yyyy}")]
        public DateTime LastModified { get; set; }
    }
}
