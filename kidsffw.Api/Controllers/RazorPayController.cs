

namespace kidsffw.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Service;
using Common.Configuration;
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


public class AcquirerData
{
    public string bank_transaction_id { get; set; }
}

public class Entity
{
    public string id { get; set; }
    public string entity { get; set; }
    public int amount { get; set; }
    public string currency { get; set; }
    public string status { get; set; }
    public string order_id { get; set; }
    public object invoice_id { get; set; }
    public bool international { get; set; }
    public string method { get; set; }
    public int amount_refunded { get; set; }
    public object refund_status { get; set; }
    public bool captured { get; set; }
    public string description { get; set; }
    public object card_id { get; set; }
    public string bank { get; set; }
    public object wallet { get; set; }
    public object vpa { get; set; }
    public string email { get; set; }
    public string contact { get; set; }
    public List<object> notes { get; set; }
    public object fee { get; set; }
    public object tax { get; set; }
    public object error_code { get; set; }
    public object error_description { get; set; }
    public object error_source { get; set; }
    public object error_step { get; set; }
    public object error_reason { get; set; }
    public AcquirerData acquirer_data { get; set; }
    public int created_at { get; set; }
}

public class Payload
{
    public Payment payment { get; set; }
}

public class Payment
{
    public Entity entity { get; set; }
}

public class Root
{
    public string entity { get; set; }
    public string account_id { get; set; }
    public string @event { get; set; }
    public List<string> contains { get; set; }
    public Payload payload { get; set; }
    public int created_at { get; set; }
}
