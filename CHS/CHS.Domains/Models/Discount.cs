using System;

namespace CHS.Domains.Models
{
    public class Discount : Common
    {
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public int DiscountedPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
