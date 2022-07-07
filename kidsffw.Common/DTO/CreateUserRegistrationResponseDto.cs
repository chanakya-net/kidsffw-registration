namespace kidsffw.Common.DTO;

public class CreateUserRegistrationResponseDto
{
    public int Id { get; set; }
    public string MobileNumber { get; set; } = string.Empty;
    public string ParentName { get; set; } = string.Empty;
    public string KidName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public decimal? Amount { get; set; } = 0M;
    public string OrderId { get; set; } = string.Empty;
    public string RazorPayKey { get; set; } = string.Empty;
}