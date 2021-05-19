using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models.Chat
{
    public class Group : Common
    {
        public string Name { get; set; }
        public bool IsLeft { get; set; }

        [ForeignKey(nameof(AdminUser))]
        public Guid AdminUserId { get; set; }
        public ApplicationUser AdminUser { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
