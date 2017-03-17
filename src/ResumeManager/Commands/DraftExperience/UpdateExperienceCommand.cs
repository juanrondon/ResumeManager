using System;

namespace ResumeManager.Commands.DraftExperience
{
    public class UpdateExperienceCommand
    {
        public int DraftExperienceId { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentlyWorking { get; set; }
        public string Description { get; set; }
    }
}