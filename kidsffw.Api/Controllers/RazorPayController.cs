namespace kidsffw.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Service;
using Common.Configuration;
using Common.DTO;
using Newtonsoft.Json;
using Razorpay.Api;

[Route("api/[controller]")]
[ApiController]
public class RazorPayController : ControllerBase
{
    private readonly IRazorPayService _razorPayService;
    private readonly RazorPayConfiguration _razorPayConfiguration;
    public RazorPayController(IRazorPayService razorPayService, RazorPayConfiguration razorPayConfiguration)
    {
        _razorPayService = razorPayService;
        _razorPayConfiguration = razorPayConfiguration;
    }

    [HttpPost("PaymentAuthorized")]
    public IActionResult PaymentAuthorized([FromBody] object payload)
    {
        //Needs to be saved in DB and check befor proceeding. If found in DB just return true from here else save and return true;
        var eventId = Request.Headers["x-razorpay-event-id "].ToString();
        var signature = Request.Headers["X-Razorpay-Signature"].ToString();
        Utils.verifyWebhookSignature(payload.ToString(), signature, "Ailudulm2@");
        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(payload.ToString());
        return Ok(200);
    }
    
    [HttpPost("PaymentFailed")]
    public IActionResult PaymentFailed([FromBody]string payload)
    {
        return Ok(200);
    }
}
