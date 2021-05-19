using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class CouponType : Common
    {
        public string TypeName { get; set; }
        public ICollection<Coupon> Coupons { get; set; }
    }
}
