namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IRazorPayService
{
    RazorPayOrderDetails CreateOrder(decimal amount);

    Task<bool> PaymentAuthorized(string paymentDetails);
    Task<bool> PaymentFailed(string paymentDetails);
}