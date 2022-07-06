namespace kidsffw.Domain.Entity;

using Base;

public class UserRegistrationEntity : BaseEntity
{
    public string MobileNumber { get; set; } = string.Empty;
    public string ParentName { get; set; } = string.Empty;
    public string KidName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool OtpVerified { get; set; } = false;
    public string CouponCode { get; set; } = string.Empty;
}
