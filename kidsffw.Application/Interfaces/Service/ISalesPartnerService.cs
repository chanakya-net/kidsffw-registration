using kidsffw.Common.DTO;

namespace kidsffw.Application.Interfaces.Service;

public interface ISalesPartnerService
{
    Task<CreateSalesPartnerResponseDto?> CreateSalesPartner(CreateSalesPartnerRequestDto? request);
    Task<SalesPartnerContactDto?> GetSalesPartnerContactByPartnerId(int salesPartnerId);
    
    Task<SalesPartnerContactDto?> GetSalesPartnerContactByCouponId(string couponId);
    Task<bool> SendRegistrationMessage(string mobileNumber, string message);
}