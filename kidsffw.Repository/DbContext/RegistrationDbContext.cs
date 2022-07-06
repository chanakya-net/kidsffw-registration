using kidsffw.Domain.Entity;

namespace kidsffw.Repository.DbContext;

using Microsoft.EntityFrameworkCore;


public class RegistrationDbContext : DbContext
{
    public DbSet<SalesPartnerEntity>? SalesPartners { get; set; }
    public DbSet<CouponEntity>? Coupons { get; set; }

    public RegistrationDbContext()
    {

    }

    public RegistrationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistrationDbContext).Assembly);

}