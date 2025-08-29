using System.ComponentModel.DataAnnotations;

namespace member_managment.Dtos
{
    public class MemberRegisterDto
{
    [Required]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
}

}
