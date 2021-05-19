using System;

namespace CHS.Domains.Models
{
    public class MemberAlert : Common
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Guid AlertTypeId { get; set; }
        public AlertType AlertType { get; set; }
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
