
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CHS.Domains.Models.Chat;

using Microsoft.AspNetCore.Identity;

namespace CHS.Domains.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public string FirstName { get; set; }
        /// <summary>
        /// Use this as Gym name for Gym Members
        /// </summary>
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool AvaiableToHireOrSpaceAvailable { get; set; } = true;
        public DateTime DOB { get; set; }
        public string DisplayName { get; set; }
        public string LogoPicture { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPicture { get; set; }
        public string CurrentLocation { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public bool GroupTrainer { get; set; }
        public bool PrivateTrainer { get; set; }
        /// <summary>
        /// In miles
        /// </summary>
        public decimal WillingToTravel { get; set; }
        /// <summary>
        /// Terms and Conditions Accept
        /// </summary>
        public bool TC_Accept { get; set; }

        public string AboutYourSelf { get; set; }

        public string Description { get; set; }
        public int Index { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public bool FilledSurveyForm { get; set; }
        public decimal YearsOfExperience { get; set; }
        public decimal YearsAsTraining { get; set; }
        public string MainSiteImage { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Timing> Timings { get; set; }

        public ICollection<TrainerLevel> TrainerLevels { get; set; }

        [ForeignKey(nameof(Gender))]
        public Guid? GenderId { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Designation> Designations { get; set; }
        public ICollection<GymMember> GymMembers { get; set; }
        public ICollection<MemberAlert> MemberAlerts { get; set; }
        public ICollection<MemberNote> MemberNotes { get; set; }
        public ICollection<MemberSubscription> MemberSubscriptions { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<MemberSkill> MemberSkills { get; set; }
        public ICollection<MemberCertification> MemberCertifications { get; set; }
        public ICollection<MemberLanguage> MemberLanguages { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        //public Guid? MemberId { get; set; }
        //public Member Member { get; set; }
    }
}
