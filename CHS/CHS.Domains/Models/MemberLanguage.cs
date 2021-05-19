using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class MemberLanguage : Common
    {
        [ForeignKey(nameof(ApplicationUser))]
        public Guid MemberId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(Language))]
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
