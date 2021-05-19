using System;

namespace CHS.Domains.Models
{
    public class Experience : Common
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
        public double NumberOfYear { get; set; }
    }
}
