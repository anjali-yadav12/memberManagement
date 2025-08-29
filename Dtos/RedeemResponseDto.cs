using System;

namespace member_managemnet.Dtos;

public class RedeemResponseDto
{
         public string? Message { get; set; }
        public int CouponID { get; set; }
        public decimal CouponValue { get; set; }
        public int RemainingPoints { get; set; }

}
