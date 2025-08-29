using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace member_managment.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(6)]
        public string? DummyOTP { get; set; }

        public bool IsVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Point> Points { get; set; } = new List<Point>();
        public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

        //Computed property for total points
        public int TotalPoints
        {
            get
            {
                int sum = 0;
                foreach (var point in Points)
                {
                    sum += point.PointsEarned;
                }
                return sum;
            }
        }
    }
}
