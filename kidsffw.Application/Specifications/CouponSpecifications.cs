namespace kidsffw.Application.Specifications;

using kidsffw.Domain.Entity;
using kidsffw.Repository.Implementation.Repository;

public static class CouponSpecifications
{
    public static BaseSpecification<CouponEntity> GetCouponCodeByCouponId(string couponCode)
    {
        return new BaseSpecification<CouponEntity>(x => x.CouponCode == couponCode);
    }

}

