using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    /// <summary>
    /// This class represents the Members who subscribe this subscription
    /// </summary>
    public class MemberSubscription : Common
    {
        //public Guid MemberId { get; set; }
        //public Member Member { get; set; }
        [ForeignKey(nameof(Subscriber))]
        public Guid SubscriberId { get; set; }
        public ApplicationUser Subscriber { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryTime { get; set; }

        public ICollection<Payment> Payments { get; set; }
        //[ForeignKey(nameof(Payment))]
        //public Guid PaymentId { get; set; }
        //public Payment Payment { get; set; }
    }
}
