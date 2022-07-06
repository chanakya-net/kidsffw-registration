namespace kidsffw.Repository.EntityTypeConfiguration;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CouponConfiguration : IEntityTypeConfiguration<CouponEntity>
{
    public void Configure(EntityTypeBuilder<CouponEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CouponCode).HasMaxLength(10).IsRequired();
        builder.Property(x => x.DiscountPercent).IsRequired();
        builder.Property(x => x.ValidFrom).IsRequired();
        builder.Property(x => x.ValidTill).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired();
    }
}
