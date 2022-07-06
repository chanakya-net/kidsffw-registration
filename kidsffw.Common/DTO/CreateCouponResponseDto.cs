using kidsffw.Domain.Entity;

namespace kidsffw.Common.DTO;

public class CreateCouponResponseDto
{
    public int Id { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTill { get; set; }
    public bool IsActive { get; set; }
    public SalesPartnerEntity SalesPartner { get; set; }
}