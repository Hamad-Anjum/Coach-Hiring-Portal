using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Timing : Common
    {
        public string Day { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

        //[ForeignKey(nameof(Member))]
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
