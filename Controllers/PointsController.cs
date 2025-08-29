using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using member_managment.Data;
using member_managemnet.Dtos;
using member_managment.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace member_managment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PointsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/points/add
        [HttpPost("add")]
        public async Task<IActionResult> AddPoints([FromBody] AddPointsDto dto)
        {
            var member = await _context.Members.FindAsync(dto.MemberID);
            if (member == null)
                return NotFound(new { message = "Member not found" });

            int pointsToAdd = (int)(dto.PurchaseAmount / 100) * 10;

            var pointEntry = new Point
            {
                MemberID = dto.MemberID,
                PointsEarned = pointsToAdd,
                TransactionDate = DateTime.UtcNow
            };

            _context.Points.Add(pointEntry);
            await _context.SaveChangesAsync();

            int totalPoints = await _context.Points
                                            .Where(p => p.MemberID == dto.MemberID)
                                            .SumAsync(p => p.PointsEarned);

            var response = new PointsResponseDto
            {
                MemberID = member.MemberID,
                Name = member.Name,
                PointsAdded = pointsToAdd,
                TotalPoints = totalPoints
            };

            return Ok(response);
        }

        // GET: api/points/{memberId}
        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetTotalPoints(int memberId)
        {
            var member = await _context.Members
                                       .Include(m => m.Points)
                                       .FirstOrDefaultAsync(m => m.MemberID == memberId);

            if (member == null)
                return NotFound(new { message = "Member not found" });

            int totalPoints = member.Points.Sum(p => p.PointsEarned);

            var response = new TotalPointsDto
            {
                MemberID = member.MemberID,
                Name = member.Name,
                TotalPoints = totalPoints
            };

            return Ok(response);
        }
    }
}
