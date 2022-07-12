namespace kidsffw.Common.DTO;

public class RazorPayPaymentDto
{
    public string MobileNumber { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public decimal AmountPaid { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public string PaymentId { get; set; } = string.Empty;
}