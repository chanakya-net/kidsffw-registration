using kidsffw.Common.Configuration;
using Microsoft.EntityFrameworkCore;

namespace kidsffw.Repository;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DbContext;

public class SqlDesignTimeFactory: IDesignTimeDbContextFactory<RegistrationDbContext>
{
    public RegistrationDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings-DesignTime.json").Build();
        var builder = new DbContextOptionsBuilder().UseSqlServer( config.GetConnectionString( "msSql" ) );
        return new RegistrationDbContext(builder.Options);
    }
}