using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Payment : Common
    {
        public decimal PayedAmount { get; set; }
        public DateTime DatePaid { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [ForeignKey(nameof(MemberSubscription))]
        public Guid MemberSubscriptionId { get; set; }
        public MemberSubscription MemberSubscription { get; set; }

        //[ForeignKey(nameof(Subscription))]
        //public Guid SubscriptionId { get; set; }
        //public Subscription Subscription { get; set; }

        //[ForeignKey(nameof(Subscriber))]
        //public Guid SubscriberId { get; set; }
        //public ApplicationUser Subscriber { get; set; }
    }
}
