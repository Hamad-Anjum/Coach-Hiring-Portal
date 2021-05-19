using System;

namespace CHS.Infrastructure.ViewModels
{
    public class MemberTrainersViewModel
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime SubscriptionDate { get; set; }
        /// <summary>
        /// Duration in months
        /// </summary>
        public int SubscriptionDuration { get; set; }
        public string Designation { get; set; }
        public string SubscriptionName { get; set; }
        public decimal Price { get; set; }
    }
}
