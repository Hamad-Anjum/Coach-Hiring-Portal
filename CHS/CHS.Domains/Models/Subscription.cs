using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class Subscription : Common
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// For training programs
        /// </summary>
        public string TrainingDays { get; set; }

        /// <summary>
        /// For training programs
        /// </summary>
        public string TrainingTiming { get; set; }
        public decimal SubscriptionMonths { get; set; }

        [ForeignKey(nameof(SubscriptionOwner))]
        public Guid SubscriptionOwnerId { get; set; }
        public ApplicationUser SubscriptionOwner { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<SubscriptionTiming> SubscriptionTimings { get; set; }
        public ICollection<GymSubscription> GymSubscriptions { get; set; }
        public ICollection<MemberSubscription> MemberSubscriptions { get; set; }
    }
}
