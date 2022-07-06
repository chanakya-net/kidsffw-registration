namespace kidsffw.Domain.Entity;

using Base;

public class OtpEntity : BaseEntity
{
    public string MobileNumber { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
    public DateTime ValidTill { get; set; }
}
