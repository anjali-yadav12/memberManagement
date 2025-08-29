using System;

namespace member_managemnet.Dtos;

public class CouponDto
{
        public int CouponID { get; set; }
        public int PointsRedeemed { get; set; }
        public decimal CouponValue { get; set; }
        public DateTime RedeemedAt { get; set; }

}
