

namespace kidsffw.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Service;

[Route("api/[controller]")]
[ApiController]
public class RazorPayController : ControllerBase
{
    private readonly IRazorPayService _razorPayService;

    public RazorPayController(IRazorPayService razorPayService) => _razorPayService = razorPayService;

    [HttpPost("PaymentAuthorized")]
    public IActionResult PaymentAuthorized([FromBody]string payload)
    {
        return Ok(200);
    }
    
    [HttpPost("PaymentFailed")]
    public IActionResult PaymentFailed([FromBody]string payload)
    {
        return Ok(200);
    }
}
