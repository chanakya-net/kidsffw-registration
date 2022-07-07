namespace kidsffw.Common.DTO;

public class RazorPayOrderDetails
{
    public string OrderId { get; set; } = string.Empty;
    public string Key { get; set; } =  string.Empty;
    public decimal? TotalAmount { get; set; } = 0M;
    public decimal? DueAmount { get; set; } = 0M;
    public string Status { get; set; } = string.Empty;
}