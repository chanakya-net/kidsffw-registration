using kidsffw.Application.Interfaces.Service;
using kidsffw.Common.DTO;
using kidsffw.Common.Interfaces.Repository;
using kidsffw.Domain.Entity;

namespace kidsffw.Application.Service;

public class SalesPartnerService : ISalesPartnerService
{
    public readonly IUnitOfWork _UnitOfWork;

    public SalesPartnerService(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }

    public async Task<CreateSalesPartnerResponseDto> CreateSalesPartner(CreateSalesPartnerRequestDto? request)
    {
        var user = await _UnitOfWork.Repository<SalesPartnerEntity>().AddAsync(
            new SalesPartnerEntity()
            {
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                Name = request.Name
            }
        );
        await _UnitOfWork.SaveChangesAsync();
        return new CreateSalesPartnerResponseDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ContactNumber = user.ContactNumber
        };
    }

    public async Task<SalesPartnerContactDto> GetSalesPartnerContact(int salesPartnerId)
    {
        var result=  await _UnitOfWork.Repository<SalesPartnerEntity>().GetByIdAsync(salesPartnerId);
        return new SalesPartnerContactDto()
        {
            ContactNumber = result.ContactNumber,
            Email = result.Email,
            Name = result.Name
        };
    }
}