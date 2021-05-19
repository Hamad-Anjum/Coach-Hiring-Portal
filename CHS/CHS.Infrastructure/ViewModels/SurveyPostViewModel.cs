using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class SurveyPostViewModel
    {
        public Guid MemberId { get; set; }

        public ICollection<SkillViewModel> Skills { get; set; }
        public ICollection<CertificationViewModel> Certifications { get; set; }

        public bool TC_Accepted { get; set; }

        /// <summary>
        /// Hourly, Daily, Monthly...
        /// </summary>
        [Required]
        public string WorkRateAs { get; set; }
        [ValidateComplexType]
        public PersonalInfoViewModel PersonalInfo { get; set; }
        [ValidateComplexType]
        public ContactViewModel Contact { get; set; }
        [ValidateComplexType]
        public TrainerComfortZoneViewModel TrainerComfortZone { get; set; }
        public TimeSlotViewModel TimeSlot { get; set; }
        [ValidateComplexType]
        public PreferredLocationViewModel PreferredLocation { get; set; }
        public ICollection<string> Designations { get; set; }
        public string Designation { get; set; }
        public string NormalizedEmail { get; set; }
    }
}
