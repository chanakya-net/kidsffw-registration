namespace kidsffw.Application.Interfaces.Service;

public interface IMessageService
{
    Task<bool> SendMessage(string mobileNumber, string message);
}