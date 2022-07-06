namespace kidsffw.Api.Controllers;

    using System.Threading.Tasks;
    using kidsffw.Application.Interfaces.Service;
    using kidsffw.Common.DTO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpPost("CreateCoupon")]
    [ProducesResponseType(typeof(CreateCouponResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewCoupon([FromBody] CreateCouponRequestDto? coupon)
    {
        if (coupon == null)
        {
            return BadRequest();
        }
        var result = await _couponService.CreateCoupon(coupon);
        return Ok(result);
    }

    [HttpGet("GetDiscountPercantage/{couponCode}")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<decimal> GetCouponDiscount(string couponCode)
    {
        var result = await _couponService.GetCouponDiscount(couponCode);
        return result;
    }
}
