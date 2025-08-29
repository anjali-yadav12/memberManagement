using System;

namespace member_managment.Models
{
    public class Coupon
    {
        public int CouponID { get; set; }
        public int MemberID { get; set; }
        public int PointsRedeemed { get; set; }
        public decimal CouponValue { get; set; }
        public DateTime RedeemedAt { get; set; }

        // Navigation
        public Member? Member { get; set; }
    }
}
