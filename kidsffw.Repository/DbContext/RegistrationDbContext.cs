namespace kidsffw.Repository.DbContext;

using Microsoft.EntityFrameworkCore;
using kidsffw.Domain.Entity;

public class RegistrationDbContext : DbContext
{
    public DbSet<SalesPartnerEntity>? SalesPartners { get; set; }
    public DbSet<CouponEntity>? Coupons { get; set; }
    public DbSet<OtpEntity>? Otps { get; set; }
    public DbSet<UserRegistrationEntity>? UserRegistrations{ get; set; }
    
    public DbSet<RazorPayErrorEntity> PaymentErrors { get; set; }
    public DbSet<RazorPayPaymentEntity> PaymentDetails { get; set; }

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