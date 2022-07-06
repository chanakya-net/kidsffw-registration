using kidsffw.Application.Interfaces.Service;
using kidsffw.Common.DTO;
using kidsffw.Common.Interfaces.Repository;
using kidsffw.Domain.Entity;

namespace kidsffw.Application.Service;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;

    public CouponService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCouponResponseDto?> CreateCoupon(CreateCouponRequestDto? request)
    {
        if (request == null)
        {
            return null;
        }

        var salesPartner = await _unitOfWork.Repository<SalesPartnerEntity>().GetByIdAsync(request.SalesPartnerId);
        if (salesPartner == null)
        {
            throw new KeyNotFoundException("SalesPartner not found");
        }
        
        var result = await _unitOfWork.Repository<CouponEntity>().AddAsync(new CouponEntity
        {
            SalesPartner = salesPartner,
            CouponCode = request.CouponCode,
            IsActive = request.IsActive,
            DiscountPercent = request.DiscountPercent,
            ValidFrom = DateTime.UtcNow
        });
        await _unitOfWork.SaveChangesAsync();
        return new CreateCouponResponseDto()
        {
            Id = result.Id,
            SalesPartner = salesPartner,
            CouponCode = result.CouponCode,
            IsActive = result.IsActive,
            DiscountPercent = result.DiscountPercent,
            ValidFrom = result.ValidFrom
        };
    }

    public Task<decimal> GetCouponDiscount(string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<SalesPartnerContactDto> GetSalesPartnerContactByCouponCode(string couponCode)
    {
        throw new NotImplementedException();
    }

    public void DisableCoupon(string couponCode)
    {
        throw new NotImplementedException();
    }
}