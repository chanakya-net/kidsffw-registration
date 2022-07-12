namespace kidsffw.Application.Service;

using kidsffw.Application.Interfaces.Service;
using Common.DTO;
using kidsffw.Common.Interfaces.Repository;
using Domain.Entity;
using Specifications;


public class SalesPartnerService : ISalesPartnerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageService _messageService;

    public SalesPartnerService(IUnitOfWork unitOfWork, IMessageService messageService)
    {
        _unitOfWork = unitOfWork;
        _messageService = messageService;
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
                ContactNumber = request.ContactNumber, Email = request.Email, Name = request.Name
            }
        );
        await _unitOfWork.SaveChangesAsync();
        return new CreateSalesPartnerResponseDto()
        {
            Id = user.Id, Name = user.Name, Email = user.Email, ContactNumber = user.ContactNumber
        };
    }

    public async Task<SalesPartnerContactDto?> GetSalesPartnerContactByPartnerId(int salesPartnerId)
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

    public async Task<SalesPartnerContactDto?> GetSalesPartnerContactByCouponId(string couponId)
    {
        var spec = Specifications.GetPartnerIdByCouponCode(couponId);
        var result = await _unitOfWork.Repository<CouponEntity>().FirstOrDefaultAsync(spec);
        if (result == null)
        {
            return null;
        }

        var salesPartnerContact = await GetSalesPartnerContactByPartnerId(result.SalesPartnerId);
        return salesPartnerContact;
    }

    public async Task<bool> SendRegistrationMessage(string mobileNumber, string message)
    {
        return await _messageService.SendMessage(mobileNumber, message);
    }
}