using kidsffw.Application.Interfaces.Service;
using kidsffw.Application.Service;
using kidsffw.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace kidsffw.Application;

public static class DependencyResolver
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        return collection.AddRepository()
            .AddScoped<ISalesPartnerService, SalesPartnerService>()
            .AddScoped<ICouponService,CouponService>();
    }
}