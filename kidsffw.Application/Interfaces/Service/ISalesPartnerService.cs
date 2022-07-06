using kidsffw.Common.DTO;

namespace kidsffw.Application.Interfaces.Service;

public interface ISalesPartnerService
{
    Task<CreateSalesPartnerResponseDto> CreateSalesPartner(CreateSalesPartnerRequestDto? request);
    Task<SalesPartnerContactDto> GetSalesPartnerContact(int salesPartnerId);
}