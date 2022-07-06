using kidsffw.Common.Interfaces.Repository;
using kidsffw.Repository.DbContext;
using kidsffw.Repository.Implementation.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace kidsffw.Repository;

public static class DependencyResolver
{
    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
    {
        var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        return serviceCollection.AddDbContext<RegistrationDbContext>(x =>
                x.UseSqlServer(config.GetConnectionString("msSql")))
            .AddTransient<IUnitOfWork, UnitOfWork>();
    }
}