namespace kidsffw.Common.DTO;

public class RazorPayErrorDto
{
    public string MobileNumber { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}