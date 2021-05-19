using System;

namespace CHS.Domains.Models
{
    public class EducationQualification : Common
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
    }
}
