using kidsffw.Domain.Base;

namespace kidsffw.Domain.Entity;

public class SalesPartnerEntity : BaseEntity
{
    public string Name { get; set; } =string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
}