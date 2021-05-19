using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class Language : Common
    {
        public string Name { get; set; }
        public ICollection<MemberLanguage> MemberLanguages { get; set; }
    }
}
