using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class Certification : Common
    {
        public string Name { get; set; }
        public ICollection<MemberCertification> MemberCertifications { get; set; }
    }
}
