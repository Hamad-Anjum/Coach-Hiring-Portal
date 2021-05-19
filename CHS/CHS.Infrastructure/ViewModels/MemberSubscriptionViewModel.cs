using System;

namespace CHS.Infrastructure.ViewModels
{
    public class MemberSubscriptionViewModel
    {
        public Guid SubscriptionId { get; set; }
        public Guid SubscriberId { get; set; }
        public decimal SubscriptionMonths { get; set; }
        public decimal PayedAmount { get; set; }
        public Guid MemberSubscriptionId { get; set; }
    }
}
