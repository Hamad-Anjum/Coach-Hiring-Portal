using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    /// <summary>
    /// Subscription Timing for Trainer Sessions
    /// </summary>
    public class SubscriptionTiming : Common
    {
        public TimeSpan From { get; set; }
        public TimeSpan? To { get; set; }

        [ForeignKey(nameof(Subscription))]
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
