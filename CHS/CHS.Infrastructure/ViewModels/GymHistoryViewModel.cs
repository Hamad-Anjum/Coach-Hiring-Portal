using System;

namespace CHS.Infrastructure.ViewModels
{
    public class GymHistoryViewModel
    {
        public Guid UserId { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int InstallmentNumber { get; set; }
    }
}
