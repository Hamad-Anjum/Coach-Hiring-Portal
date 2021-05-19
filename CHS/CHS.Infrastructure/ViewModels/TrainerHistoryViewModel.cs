using System;

namespace CHS.Infrastructure.ViewModels
{
    public class TrainerHistoryViewModel
    {
        public string UserId { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int InstallmentNumber { get; set; }
    }
}
