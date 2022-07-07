namespace kidsffw.Domain.Entity;

using Base;
public class CouponEntity: BaseEntity
{
    public string CouponCode { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTill { get; set; }
    public bool IsActive { get; set; }
    public SalesPartnerEntity SalesPartner { get; set; }
    
    public int SalesPartnerId { get; set; }
}