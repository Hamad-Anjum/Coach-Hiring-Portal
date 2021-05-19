using System;

namespace CHS.Domains.Models
{
    public class PaymentDetails : Common
    {
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
