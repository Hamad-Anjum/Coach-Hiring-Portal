using System;

namespace CHS.Domains.Models
{
    public class Designation : Common
    {
        public string Title { get; set; }
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
