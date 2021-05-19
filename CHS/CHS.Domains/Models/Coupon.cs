using System;

namespace CHS.Domains.Models
{
    public class Coupon : Common
    {
        public string CouponCode { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid CouponTypeId { get; set; }
        public CouponType CouponType { get; set; }
    }
}
