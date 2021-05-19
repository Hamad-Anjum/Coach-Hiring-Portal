using System;

namespace CHS.Domains.Models
{
    public class GymSubscription : Common
    {
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public Guid GymId { get; set; }
        public Gym Gym { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
