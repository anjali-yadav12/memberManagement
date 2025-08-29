using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using member_managment.Data;
using member_managment.Models;
using member_managment.Dtos;
using member_managment.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace member_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public MemberController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ Register Member
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MemberRegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var member = new Member
            {
                Name = dto.Name,
                MobileNumber = dto.MobileNumber,
                DummyOTP = "123456", // Dummy OTP (replace with real SMS/OTP service in prod)
                CreatedAt = DateTime.UtcNow,
                IsVerified = false
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            var response = new MemberResponseDto
            {
                MemberID = member.MemberID,
                Name = member.Name,
                MobileNumber = member.MobileNumber,
                IsVerified = member.IsVerified
            };

            return Ok(new { message = "OTP sent (dummy)", data = response });
        }

        // ✅ Verify OTP and generate JWT
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] MemberVerifyOtpDto dto)
        {
            var member = await _context.Members.FindAsync(dto.MemberID);
            if (member == null) return NotFound("Member not found");

            if (member.DummyOTP == dto.Otp)
            {
                member.IsVerified = true;
                member.DummyOTP = null;
                await _context.SaveChangesAsync();

                // 🔑 Use config values instead of hardcoding
                var token = JwtTokenHelper.GenerateToken(
                    member,
                    _configuration["Jwt:Key"]!,
                    _configuration["Jwt:Issuer"]!,
                    _configuration["Jwt:Audience"]!
                );

                var response = new MemberResponseDto
                {
                    MemberID = member.MemberID,
                    Name = member.Name,
                    MobileNumber = member.MobileNumber,
                    IsVerified = member.IsVerified
                };

                return Ok(new { message = "Verified successfully", token, data = response });
            }

            return BadRequest(new { message = "Invalid OTP" });
        }

        // ✅ Example Protected Route
        [HttpGet("profile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetProfile(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null) return NotFound();

            return Ok(new 
            { 
                Name = member.Name, 
                Mobile = member.MobileNumber, 
                Verified = member.IsVerified 
            });
        }
    }
}
