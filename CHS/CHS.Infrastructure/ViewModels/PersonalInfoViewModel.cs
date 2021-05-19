using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class PersonalInfoViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Guid GenderId { get; set; }
        //public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public decimal YearsOfExperience { get; set; }

        [Required]
        public decimal YearsAsTraining { get; set; }

        [Required]
        public decimal MinPrice { get; set; }
        [Required]
        public decimal MaxPrice { get; set; }

        public bool EverConvicted { get; set; }
        public bool GulfCitizen { get; set; }
        public bool AllowedToWork { get; set; }
        public bool GroupTrainer { get; set; }
        public bool PrivateTrainer { get; set; }
        public string ProfilePicture { get; set; }
        public string AboutMySelf { get; set; }
        public string YoutubeUrl { get; set; }
        public string Hobbies { get; set; }
        public int Age { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public decimal Weight { get; set; }
        public bool CompleteWizard { get; set; }
        public string Description { get; set; }
        public bool FilledSurveyForm { get; set; }
        public string CoverPicture { get; set; }
        public string LogoPicture { get; set; }
    }
}
