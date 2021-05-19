using System;
using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class SurveyGetViewModel
    {
        public Guid MemberId { get; set; }
        public double YearsOfExperience { get; set; }
        public double YearsAsTraining { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; }
        public ICollection<CertificationViewModel> Certifications { get; set; }
        public TimeSlotViewModel TimeSlot { get; set; }
        public PreferredLocationViewModel PreferredLocation { get; set; }
        public ICollection<CountryViewModel> Countries { get; set; }
        public ICollection<GenderViewModel> Genders { get; set; }
    }
}
