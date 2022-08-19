// ReSharper disable InconsistentNaming
namespace kidsffw.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Service;
using Common.DTO;
using Newtonsoft.Json;
using Razorpay.Api;

[Route("api/[controller]")]
[ApiController]
public class RazorPayController : ControllerBase
{
    private readonly IRazorPayErrorService _razorPayErrorService;
    private readonly IRazorPayPaymentService _razorPayPaymentService;
    private readonly IMessageService _messageService;
    private readonly IUserRegistrationService _userRegistrationService;
    private readonly ISalesPartnerService _salesPartnerService;
    public RazorPayController(IRazorPayPaymentService razorPayPaymentService, IRazorPayErrorService razorPayErrorService, IMessageService messageService, IUserRegistrationService userRegistrationService, ISalesPartnerService salesPartnerService)
    {
        _razorPayPaymentService = razorPayPaymentService;
        _razorPayErrorService = razorPayErrorService;
        _messageService = messageService;
        _userRegistrationService = userRegistrationService;
        _salesPartnerService = salesPartnerService;
    }

    [HttpPost("PaymentAuthorized")]
    public async Task<IActionResult> PaymentAuthorized([FromBody] object payload)
    {
        //Needs to be saved in DB and check before proceeding. If found in DB just return true from here else save and return true;
        try
        {
            // Get Event ID from from request
            var eventId = Request.Headers["x-razorpay-event-id"].ToString();
            //check if event is already saved 
            var eventIdResult = await _razorPayPaymentService.FindEventId(eventId);
            if (eventIdResult == eventId)
            {
                return Ok(200);
            }
            
            // get signature from request
            var signature = Request.Headers["X-Razorpay-Signature"].ToString();
            // Verify if signature is valid
            Utils.verifyWebhookSignature(payload.ToString(), signature, "Ailudulm2@");
            
            // Get the payment object from the payload
            Root myDeserializedOrder = JsonConvert.DeserializeObject<Root>(payload.ToString() ?? string.Empty) ?? throw new InvalidOperationException();
            
            var orderId = myDeserializedOrder.payload?.payment?.entity?.order_id;
            var paymentId = myDeserializedOrder.payload?.payment?.entity?.id;
            if (orderId != null)
            {
                //Get registration details from DB
                var registrationDetails = await _userRegistrationService.GetUserByOrderId(orderId);
                if (registrationDetails != null)
                {
                    // Find sales partner by coupon code
                    var salesPartner =
                        await _salesPartnerService.GetSalesPartnerContactByCouponId(registrationDetails.CouponCode);

                    // Send message to sales partner
                    if (salesPartner != null)
                    {
#pragma warning disable CS4014
                        _messageService.SendMessage(salesPartner.ContactNumber ?? string.Empty,

                            $"Hi {salesPartner.Name},\n Mr./Mrs. {registrationDetails.ParentName} has registered successfully with coupon {registrationDetails.CouponCode} for {registrationDetails.City} event.\n Thank you for your contribution.");
                    }

                    // send message to customer
                    _messageService.SendMessage(registrationDetails.MobileNumber,
                        $"Hi {registrationDetails.ParentName},\n Your kid {registrationDetails.KidName} has been registered successfully for {registrationDetails.City} event.\n Your Payment id {paymentId}");
                    _messageService.SendMessage("9513212352",
                        $"Hi Prakash, {registrationDetails.ParentName} with contact number {registrationDetails.MobileNumber} has registered his/her kid {registrationDetails.KidName} successfully for {registrationDetails.City} event.\n");
#pragma warning restore CS4014

                }

                // Save the event id in DB
                var _ = await _razorPayPaymentService.SavePaymentInformation(
                    new RazorPayPaymentDto()
                    {
                        AmountPaid = myDeserializedOrder.payload!.payment!.entity!.amount,
                        EventId = eventId,
                        OrderId = myDeserializedOrder.payload.payment.entity.order_id,
                        MobileNumber = myDeserializedOrder.payload.payment.entity.contact,
                        PaymentId = paymentId!,
                    });
            }
            
        }
        catch(Exception ex)
        {
            await _razorPayErrorService.SaveErrorInformation(new RazorPayErrorDto()
            {
                ErrorMessage = ex.Message,
                EventId =  "Unable to fetch",
                OrderId = "Invalid Order Id",
                MobileNumber = "paymentId",
            });
        }
        return Ok(200);
    }
    
    [HttpPost("PaymentFailed")]
    public async Task<IActionResult> PaymentFailed([FromBody]object payload)
    {
        // Get Event ID from from request
        var eventId = Request.Headers["x-razorpay-event-id"].ToString();
        if (eventId.Length <= 0)
        {
            return Ok(200);
        }
        //check if event is already saved 
        var eventIdResult = await _razorPayErrorService.FindEventId(eventId);
        if (eventIdResult == eventId)
        {
            return Ok(200);
        }
       
        // get signature from request
        var signature = Request.Headers["X-Razorpay-Signature"].ToString();
        // Verify if signature is valid
        Utils.verifyWebhookSignature(payload.ToString(), signature, "Ailudulm2@");
            
        // Get the payment object from the payload
        Root myDeserializedOrder = JsonConvert.DeserializeObject<Root>(payload.ToString() ?? string.Empty)!;
        if (myDeserializedOrder.payload?.payment?.entity?.contact.Length > 0)
        {
            var orderId = myDeserializedOrder.payload.payment.entity.order_id;
            var registrationDetails = await _userRegistrationService.GetUserByOrderId(orderId);
            if (registrationDetails != null)
            {
#pragma warning disable CS4014
                _messageService.SendMessage("9513212352",

                    $"{registrationDetails.ParentName} with contact number {registrationDetails.MobileNumber} tried to register for {registrationDetails.City} event but failed.");
                _messageService.SendMessage("9620880000",
                    $"{registrationDetails.ParentName} with contact number {registrationDetails.MobileNumber} tried to register for {registrationDetails.City} event but failed.");
                var _ = await _razorPayErrorService.SaveErrorInformation(
                    new
                        RazorPayErrorDto()
                        {
                            ErrorMessage = "Payment Failed",
                            EventId = eventId,
                            OrderId = orderId,
                            MobileNumber = registrationDetails.MobileNumber,
                        });
#pragma warning restore CS4014
            }
                
        }
        return Ok(200);  
    }
}


public class AcquirerData
{
    public string bank_transaction_id { get; set; } = string.Empty;
}

public class Entity
{
    public string id { get; set; } = string.Empty;
    public string entity { get; set; }= string.Empty;
    public int amount { get; set; }
    public string currency { get; set; }= string.Empty;
    public string status { get; set; }= string.Empty;
    public string order_id { get; set; }= string.Empty;
    public object invoice_id { get; set; }= string.Empty;
    public bool international { get; set; }    
    public string method { get; set; }= string.Empty;
    public int amount_refunded { get; set; }
    public object? refund_status { get; set; }
    public bool? captured { get; set; }
    public string description { get; set; } = string.Empty;
    public object? card_id { get; set; }
    public string? bank { get; set; }
    public object? wallet { get; set; }
    public object? vpa { get; set; }
    public string? email { get; set; }
    public string contact { get; set; } = string.Empty;
    public object? fee { get; set; }
    public object? tax { get; set; }
    public object? error_code { get; set; }
    public object? error_description { get; set; }
    public object? error_source { get; set; }
    public object? error_step { get; set; }
    public object? error_reason { get; set; }
    public object? acquirer_data { get; set; }
    public int created_at { get; set; }
}

public class Payload
{
    public Payment? payment { get; set; }
}

public class Payment
{
    public Entity? entity { get; set; }
}

public class Root
{
    public string entity { get; set; }= string.Empty;
    public string account_id { get; set; }= string.Empty;
    public string @event { get; set; }= string.Empty;
    public List<string>? contains { get; set; }
    public Payload? payload { get; set; }
    public int created_at { get; set; }
}