using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public Guid Id { get; set; }
        public bool FilledSurveyForm { get; set; }
    }
}
