namespace kidsffw.Common.DTO;

public class CreateUserRegistrationRequestDto
{
    public string MobileNumber { get; set; } = string.Empty;
    public string ParentName { get; set; } = string.Empty;
    public string KidName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
}