using System;

namespace CHS.Domains.Models
{
    public class MemberNote : Common
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
