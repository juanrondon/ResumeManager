using System.ComponentModel.DataAnnotations;

namespace ResumeManager.DataAccess.Enums
{
    public enum LanguageProficiency
    {
        [Display(Name = "Elementary Proficiency")]
        ElementaryProficiency,
        [Display(Name = "Limited Working Proficiency")]
        LimitedWorkingProficiency,
        [Display(Name = "Professional Working Proficiency")]
        ProfessionalWorkingProficiency,
        [Display(Name = "Full Professional Proficiency")]
        FullProfessionalProficiency,
        [Display(Name = "Native Or Bilingual Proficiency")]
        NativeOrBilingualProficiency
    }
}
