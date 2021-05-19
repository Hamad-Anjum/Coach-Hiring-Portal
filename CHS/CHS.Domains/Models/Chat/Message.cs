using System;

namespace CHS.Domains.Models.Chat
{
    public class Message : Common
    {
        public string Text { get; set; }
        public bool IsLiked { get; set; }
        public Guid SenderUserId { get; set; }
        public ApplicationUser SenderUser { get; set; }
        public Guid ReceiverUserId { get; set; }
        public ApplicationUser ReceiverUser { get; set; }
    }
}
