using System;

namespace CHS.Infrastructure.ViewModels
{
    public class CompleteCourseViewModel
    {
        public Guid SubscriptionId { get; set; }
        public Guid MemberSubscriptionId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
