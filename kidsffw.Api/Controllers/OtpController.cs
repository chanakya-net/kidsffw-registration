namespace kidsffw.Api.Controllers;

using Application.Interfaces.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class OtpController : ControllerBase
{
    private readonly IOtpService _otpService;

    public OtpController(IOtpService otpService) => _otpService = otpService;

    [HttpPost("Get")]
    public async Task<IActionResult> GetOtp(string mobileNumber)
    {
        var result = await _otpService.CreateOtp(mobileNumber);
        return Ok(result);
    }

    [HttpPost("Verify")]
    public async Task<IActionResult> VerifyOtp(string otp, string mobileNumber)
    {
        var result = await _otpService.VerifyOtp(mobileNumber, otp);
        return Ok(result);
    }

}
