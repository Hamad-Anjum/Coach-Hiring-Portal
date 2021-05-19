using System;
using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class MemberStudentsViewModel
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        /// <summary>
        /// Duration in months
        /// </summary>
        public decimal SubscriptionDuration { get; set; }
        public string Designation { get; set; }
        public string SubscriptionName { get; set; }
        public decimal Price { get; set; }
        public ICollection<PaymentViewModel> Payments { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime LastPresentDate { get; set; }
        public bool IsStillActive { get; set; }
        public Guid SubscriptionId { get; set; }
    }

    public class PaymentViewModel
    {
        public decimal PayedAmount { get; set; }
        public DateTime PayDate { get; set; }
        public Guid MemberSubscriptionId { get; set; }
    }
}
