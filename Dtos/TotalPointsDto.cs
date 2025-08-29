using System;

namespace member_managemnet.Dtos;

public class TotalPointsDto
{
        public int MemberID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalPoints { get; set; }

}
