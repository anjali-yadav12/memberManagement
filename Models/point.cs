using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace member_managment.Models
{
    public class Point
    {
        public int PointID { get; set; }
        [ForeignKey("Member")]
        public int MemberID { get; set; }
        [Required]
        public int PointsEarned { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public Member? Member { get; set; }
    }
}
