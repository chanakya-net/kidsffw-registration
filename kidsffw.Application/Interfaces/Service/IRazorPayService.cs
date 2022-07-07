namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IRazorPayService
{
    RazorPayOrderDetails CreateOrder(decimal amount);
}