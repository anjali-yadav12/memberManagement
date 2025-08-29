namespace member_managment.Dtos
{
    public class MemberResponseDto
    {
        public int MemberID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
    }
}
