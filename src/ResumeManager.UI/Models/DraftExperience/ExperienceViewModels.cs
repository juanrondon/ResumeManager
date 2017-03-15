namespace ResumeManager.UI.Models.DraftExperience
{
    public class ExperienceViewModels
    {
        public AddExperienceViewModel AddExperienceViewModel { get; set; }
        public UpdateExperienceViewModel UpdateExperienceViewModel { get; set; }

        public ExperienceViewModels()
        {
            AddExperienceViewModel = new AddExperienceViewModel();
            UpdateExperienceViewModel = new UpdateExperienceViewModel();
        }
    }
}
