using kidsffw.Application.Interfaces.Service;
using kidsffw.Common.DTO;
using kidsffw.Common.Interfaces.Repository;
using kidsffw.Domain.Entity;

namespace kidsffw.Application.Service;

public class SalesPartnerService : ISalesPartnerService
{
    private readonly IUnitOfWork _unitOfWork;

    public SalesPartnerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateSalesPartnerResponseDto?> CreateSalesPartner(CreateSalesPartnerRequestDto? request)
    {
        if (request == null)
        {
            return null;
        }
        var user = await _unitOfWork.Repository<SalesPartnerEntity>().AddAsync(
            new SalesPartnerEntity()
            {
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                Name = request.Name
            }
        );
        await _unitOfWork.SaveChangesAsync();
        return new CreateSalesPartnerResponseDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ContactNumber = user.ContactNumber
        };
    }

    public async Task<SalesPartnerContactDto?> GetSalesPartnerContact(int salesPartnerId)
    {
        var result=  await _unitOfWork.Repository<SalesPartnerEntity>().GetByIdAsync(salesPartnerId);
        if (result == null)
        {
            return null;
        }
        return new SalesPartnerContactDto()
        {
            ContactNumber = result.ContactNumber,
            Email = result.Email,
            Name = result.Name
        };
    }
}