namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IRazorPayPaymentService
{
    public Task<string?> FindEventId(string eventId);
    public Task<int> SavePaymentInformation(RazorPayPaymentDto payment);
}