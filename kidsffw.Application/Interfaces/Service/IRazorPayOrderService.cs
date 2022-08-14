namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IRazorPayOrderService
{
    public RazorPayOrderDetails CreateOrder(decimal amount,string name, string contactNumber);
}