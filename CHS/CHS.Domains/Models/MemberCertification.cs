using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class MemberCertification : Common
    {
        //[ForeignKey(nameof(Member))]
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(Certification))]
        public Guid CertificationId { get; set; }
        public Certification Certification { get; set; }
    }
}
