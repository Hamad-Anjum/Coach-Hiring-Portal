using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class ContactViewModel
    {
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string LinkedInUrl { get; set; }
        public string FaceBookUrl { get; set; }
        public Guid CountryId { get; set; }
        public string Country { get; set; }
        public Guid StateId { get; set; }
        public string State { get; set; }
        public Guid CityId { get; set; }
        public string City { get; set; }
        public Guid DistrictId { get; set; }
        public string District { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers are allowed")]
        public string ZipCode { get; set; }
    }
}
