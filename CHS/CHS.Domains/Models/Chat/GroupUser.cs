using System;

namespace CHS.Domains.Models.Chat
{
    public class GroupUser : Common
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
