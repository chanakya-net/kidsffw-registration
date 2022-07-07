namespace kidsffw.Application.Service;

using Interfaces.Service;
using RestSharp;

public class WhatsAppMessageService : IMessageService
{
    //http://wa.asapsms.in:8080/SendMessage?wa=919708200200&msg=Hello
    public const string WhatsAppApiUrl = @"http://wa.asapsms.in:8080/SendMessage";

    public async Task<bool> SendMessage(string mobileNumber, string message)
    {
        var options = new RestClientOptions(WhatsAppApiUrl) {
            ThrowOnAnyError = true
        };
        var whatsAppClient = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddQueryParameter("wa", mobileNumber);
        message = "Your OTP for registration for KFFW event is:-\n\n" + message;
        request.AddQueryParameter("msg", message);
        var response = await whatsAppClient.ExecuteAsync(request);
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}