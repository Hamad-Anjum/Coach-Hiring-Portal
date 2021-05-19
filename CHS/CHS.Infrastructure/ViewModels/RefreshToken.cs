using System;

namespace CHS.Infrastructure.ViewModels
{
    public class RefreshToken
    {
        public Guid TokenId { get; } = Guid.NewGuid();
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
}
