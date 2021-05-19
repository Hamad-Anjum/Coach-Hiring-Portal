using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class Skill : Common
    {
        public string Name { get; set; }
        public ICollection<MemberSkill> MemberSkills { get; set; }
    }
}
