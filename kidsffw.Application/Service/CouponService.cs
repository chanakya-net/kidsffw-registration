namespace kidsffw.Application.Service;

using kidsffw.Application.Interfaces.Service;
using Specifications;
using Common.DTO;
using kidsffw.Common.Interfaces.Repository;
using Domain.Entity;
using Mapster;

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

        var couponEntity = request.Adapt<CouponEntity>();
        couponEntity.SalesPartner = salesPartner;
        var result = await _unitOfWork.Repository<CouponEntity>().AddAsync(couponEntity);
        await _unitOfWork.SaveChangesAsync();
        return new CreateCouponResponseDto()
        {
            Id = result.Id,
            SalesPartnerId = salesPartner.Id,
            CouponCode = result.CouponCode,
            IsActive = result.IsActive,
            DiscountPercent = result.DiscountPercent,
            ValidFrom = result.ValidFrom
        };
    }

    public async Task<decimal> GetCouponDiscount(string couponCode)
    {
        var spec = Specifications.GetCouponCodeByCouponId(couponCode);
        var result = await _unitOfWork.Repository<CouponEntity>().FirstOrDefaultAsync(spec);
        if (result!=null)
        {
            return result.DiscountPercent;
        }
        return 0;
    }

    public void DisableCoupon(string couponCode)
    {
       return;
    }
}