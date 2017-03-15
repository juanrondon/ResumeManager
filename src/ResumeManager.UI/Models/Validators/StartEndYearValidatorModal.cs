using System.ComponentModel.DataAnnotations;

namespace ResumeManager.UI.Models.Validators
{
    public class StartEndYearValidatorModal : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.DraftEducation.UpdateEducationViewModel)validationContext.ObjectInstance;
            if (model.FromYear != null && model.ToYear != null)
            {
                var fromYear = model.FromYear.Value;
                var toYear = model.ToYear.Value;
                if (toYear < fromYear)
                {
                    return new ValidationResult(string.Format("Year can't be less than {0}", fromYear));
                }
            }
            return ValidationResult.Success;
        }
    }
}
