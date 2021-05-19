using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class BasicInfoViewModel
    {
        public Guid MemberId { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ReTypePassword { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public Guid ContactId { get; set; }
        public string ContactNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool AvailableOrHire { get; set; }
        public Guid AddressId { get; set; }
        public string Address { get; set; }
        public string TellAboutYou { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public Guid DistrictId { get; set; }
        public string DistrictName { get; set; }
        public decimal WillingToTravel { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public bool GroupTrainer { get; set; }
        public bool PrivateTrainer { get; set; }
        public DateTime DOB { get; set; }
        public Guid GenderId { get; set; }
        public string GenderName { get; set; }
        public bool AddressIsActive { get; set; }
        public bool AddressIsDelete { get; set; }
        public int AddressIndex { get; set; }
        public DateTime? AddressCreatedAt { get; set; }
        public string AddressCreatedBy { get; set; }
        public string Description { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public bool TC_Accepted { get; set; }
        public bool AvaiableToHireOrSpaceAvailable { get; set; }
        public string CoverPicture { get; set; }
        public string ProfilePicture { get; set; }
        public int Followers { get; set; }
        public int Followings { get; set; }
    }
}
