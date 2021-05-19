using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models.Chat
{
    public class MessageLike : Common
    {
        [ForeignKey(nameof(GroupMessage))]
        public Guid GroupMessageId { get; set; }
        public GroupMessage GroupMessage { get; set; }
        public Guid LikeeUserId { get; set; }
        public ApplicationUser LikeeUser { get; set; }
    }
}
