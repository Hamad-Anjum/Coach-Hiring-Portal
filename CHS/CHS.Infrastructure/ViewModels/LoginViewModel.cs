using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public string[] Roles { get; set; }

        public bool RememberMe { get; set; }
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public bool FilledSurveyForm { get; set; }
        public string ProfileImage { get; set; }
    }
}
