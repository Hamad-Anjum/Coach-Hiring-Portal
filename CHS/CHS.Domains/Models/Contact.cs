using System;

namespace CHS.Domains.Models
{
    public class Contact : Common
    {
        public string Number { get; set; }
        public string Email { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
        public Guid? GymId { get; set; }
        public Gym Gym { get; set; }
        //public Guid ContactTypeId { get; set; }
        //public ContactType ContactType { get; set; }
    }
}
