using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kidsffw.Application.Interfaces.Service;
using kidsffw.Application.Service;
using kidsffw.Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kidsffw.Api.Controllers
{
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
        public async Task<IActionResult> CreateNewCoupon([FromBody] CreateCouponRequestDto? coupon)
        {
            if (coupon == null)
            {
                return BadRequest();
            }
            var result = await _couponService.CreateCoupon(coupon);
            return Ok(result);
        }
    }
}
