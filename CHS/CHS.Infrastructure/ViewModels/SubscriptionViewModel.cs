using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class SubscriptionViewModel
    {
        public Guid SubscriptionId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        public string Picture { get; set; }

        [Required]
        public decimal SubscriptionMonths { get; set; }

        public string TrainingDays { get; set; }

        [Required]
        public string TrainingTiming { get; set; }
        public string Detail { get; set; }
        public string Images { get; set; }
        public Guid SubscriptionOwnerMemberId { get; set; }
        public string Description { get; set; }
        public int Index { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
