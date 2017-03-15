namespace ResumeManager.UI.Models.DraftEducation
{
    public class EducationViewModels
    {
        public AddEducationViewModel AddEducationViewModel { get; set; }
        public UpdateEducationViewModel UpdateEducationViewModel { get; set; }

        public EducationViewModels()
        {
            AddEducationViewModel = new AddEducationViewModel();
            UpdateEducationViewModel = new UpdateEducationViewModel();
        }
    }
}
