using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeManager.Commands.ResumeDraft
{
    public class UpdateQualificationCommand
    {
        public int DraftQualId { get; set; }        
        public string Name { get; set; }
        public string Type { get; set; }
        public string Institution { get; set; }
        public DateTime DateAquired { get; set; }
        public string OtherInfo { get; set; }
    }
}
