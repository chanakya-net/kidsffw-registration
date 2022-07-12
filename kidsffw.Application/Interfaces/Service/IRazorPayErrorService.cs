namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IRazorPayErrorService
{
    public Task<string?> FindEventId(string eventId);
    public Task<int> SaveErrorInformation(RazorPayErrorDto payment);
}