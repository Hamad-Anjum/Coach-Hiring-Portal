using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Feedback : Common
    {
        [ForeignKey(nameof(Subscription))]
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public decimal Rating { get; set; }
        public string Comments { get; set; }
    }
}
