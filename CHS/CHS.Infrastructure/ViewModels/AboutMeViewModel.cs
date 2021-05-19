using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class AboutMeViewModel
    {
        public decimal YearsOfExperience { get; set; }
        public decimal YearsAsTraining { get; set; }
        public BasicInfoViewModel BasicInfo { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; }
        public ICollection<LanguageViewModel> Languages { get; set; }
    }
}
