using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.UI.Models.ResumeDraft
{
    public class UpdateQualificationViewModel
    {       
        public string Name { get; set; }
        public string Type { get; set; }
        public string Institution { get; set; }
        public DateTime DateAquired { get; set; }
        public string OtherInfo { get; set; }
        public int DraftQualificationId { get; set; }
    }
}
