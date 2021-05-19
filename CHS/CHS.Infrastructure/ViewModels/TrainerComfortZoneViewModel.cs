using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class TrainerComfortZoneViewModel
    {
        /// <summary>
        /// Willing to travel in miles
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public decimal WillingToTravel { get; set; }
        /// <summary>
        /// Preferred Student's age group From
        /// </summary>
        public int StudentAgeGroupFrom { get; set; }
        /// <summary>
        /// Preferred Student's age group To
        /// </summary>
        public int StudentAgeGroupTo { get; set; }
        /// <summary>
        /// Preferred Student's Skill Level
        /// </summary>
        public string SkillLevel { get; set; }

        /// <summary>
        /// Preferred Student's with following goals
        /// </summary>
        public string StudentGoals { get; set; }
    }
}
