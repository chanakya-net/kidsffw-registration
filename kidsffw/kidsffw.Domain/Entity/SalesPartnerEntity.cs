using kidsffw.Domain.Base;

namespace kidsffw.Domain.Entity;

public class SalesPartnerEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
}