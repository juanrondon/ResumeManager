namespace ResumeManager.UI.Models.DraftQualification
{
    public class QualificationViewModels
    {
        public AddQualificationViewModel AddQualificationViewModel { get; set; }
        public UpdateQualificationViewModel UpdateQualificationViewModel { get; set; }
        public QualificationViewModels()
        {
            AddQualificationViewModel = new AddQualificationViewModel();
            UpdateQualificationViewModel = new UpdateQualificationViewModel();
        }
    }
}
