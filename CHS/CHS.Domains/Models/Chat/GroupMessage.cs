using System;

namespace CHS.Domains.Models.Chat
{
    public class GroupMessage : Common
    {
        public string Text { get; set; }
        public Guid SenderUserId { get; set; }
        public ApplicationUser SenderUser { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
