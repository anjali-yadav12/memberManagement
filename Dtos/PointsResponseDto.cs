using System;

namespace member_managemnet.Dtos;

public class PointsResponseDto
{
        public int MemberID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PointsAdded { get; set; }
        public int TotalPoints { get; set; }
}
