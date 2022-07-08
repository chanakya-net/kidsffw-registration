namespace kidsffw.Application.Service;

using Common.Configuration;
using Common.DTO;
using Interfaces.Service;
using Razorpay.Api;

public class RazorPayService : IRazorPayService
{
    private readonly RazorPayConfiguration _razorPayConfiguration;

    public RazorPayService(RazorPayConfiguration razorPayConfiguration) => _razorPayConfiguration = razorPayConfiguration;

    public RazorPayOrderDetails CreateOrder(decimal amount)
    {
        var orderOptions = new Dictionary<string, object>();
        var razorPayClient = new RazorpayClient(_razorPayConfiguration.KeyId, _razorPayConfiguration.KeySecret);
        orderOptions.Add("amount", amount);
        orderOptions.Add("currency", "INR");
        orderOptions.Add("receipt", "<<we need to add>>");
        Order orderCreated = razorPayClient.Order.Create(orderOptions);
        var orderDetails = new RazorPayOrderDetails()
        {
            Key = _razorPayConfiguration.KeyId, OrderId = orderCreated.Attributes["id"], 
            Status = orderCreated.Attributes["status"],
            DueAmount = orderCreated.Attributes["amount_due"], 
            TotalAmount = orderCreated.Attributes["amount"]
        };
        return orderDetails;
    }

    public async Task<bool> PaymentAuthorized(string paymentDetails) => throw new NotImplementedException();

    public async Task<bool> PaymentFailed(string paymentDetails) => throw new NotImplementedException();
}