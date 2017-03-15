using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.UI.Models.DraftEducation
{
    public class PreviewEducationViewModel
    {
        public string School { get; set; }
      
        public string Degree { get; set; }
       
        public string FieldOfStudy { get; set; }

        public string Grade { get; set; }
       
        public int? FromYear { get; set; }
        
        public int? ToYear { get; set; }

        public string Description { get; set; }
    }
}
