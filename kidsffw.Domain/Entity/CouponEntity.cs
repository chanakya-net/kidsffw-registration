using kidsffw.Domain.Base;

namespace kidsffw.Domain.Entity;

public class CouponEntity: BaseEntity
{
    public string CouponCode { get; set; }
    public decimal DiscountPercent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTill { get; set; }
    public bool IsActive { get; set; }
    public SalesPartnerEntity SalesPartner { get; set; }
}