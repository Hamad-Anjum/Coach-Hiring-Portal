using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Address : Common
    {
        public string AddressTitle { get; set; }
        public string Detail { get; set; }
        //public Guid? MemberId { get; set; }
        //public Member Member { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public Guid? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid? GymId { get; set; }
        public Gym Gym { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        public Guid? DistrictId { get; set; }
        public District District { get; set; }
        //public Guid AddressTypeId { get; set; }
        //public AddressType AddressType { get; set; }
    }
}
