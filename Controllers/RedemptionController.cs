using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using member_managment.Data;
using member_managment.Models;
using System.Linq;
using System.Threading.Tasks;
using member_managemnet.Dtos;

[Route("api/[controller]")]
[ApiController]
public class RedemptionController : ControllerBase
{
    private readonly AppDbContext _context;

    public RedemptionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("redeem")]
    public async Task<IActionResult> RedeemPoints([FromBody] RedeemRequestDto request)
    {
        var member = await _context.Members.FindAsync(request.MemberId);
        if (member == null)
            return NotFound(new { Message = "Member not found" });

        // Calculate available points
        var totalPoints = _context.Points
            .Where(p => p.MemberID == request.MemberId)
            .Sum(p => p.PointsEarned);

        if (totalPoints < request.PointsToRedeem)
            return BadRequest(new { Message = "Insufficient points" });

        decimal couponValue = request.PointsToRedeem switch
        {
            500 => 50,
            1000 => 100,
            _ => 0
        };

        if (couponValue == 0)
            return BadRequest(new { Message = "Invalid redemption amount" });

        // Deduct points FIFO
        var memberPoints = _context.Points
            .Where(p => p.MemberID == request.MemberId)
            .OrderBy(p => p.PointID)
            .ToList();

        int pointsLeftToDeduct = request.PointsToRedeem;
        foreach (var point in memberPoints)
        {
            if (pointsLeftToDeduct <= 0) break;

            if (point.PointsEarned <= pointsLeftToDeduct)
            {
                pointsLeftToDeduct -= point.PointsEarned;
                _context.Points.Remove(point);
            }
            else
            {
                point.PointsEarned -= pointsLeftToDeduct;
                pointsLeftToDeduct = 0;
                _context.Points.Update(point);
            }
        }

        // Create coupon record
        var coupon = new Coupon
        {
            MemberID = request.MemberId,
            PointsRedeemed = request.PointsToRedeem,
            CouponValue = couponValue,
            RedeemedAt = DateTime.UtcNow
        };
        _context.Coupons.Add(coupon);

        await _context.SaveChangesAsync();

        int remainingPoints = totalPoints - request.PointsToRedeem;

        return Ok(new
        {
            Message = $"Successfully redeemed {request.PointsToRedeem} points.",
            CouponID = coupon.CouponID,
            CouponValue = couponValue,
            RemainingPoints = remainingPoints
        });

    }

    [HttpGet("member/{memberId}/coupons")]
    public async Task<IActionResult> GetRedeemedCoupons(int memberId)
    {
        var coupons = await _context.Coupons
            .Where(c => c.MemberID == memberId)
            .Select(c => new
            {
                c.CouponID,
                c.PointsRedeemed,
                c.CouponValue,
                c.RedeemedAt
            })
            .ToListAsync();

        return Ok(coupons); // returns [] if none
    }

}
