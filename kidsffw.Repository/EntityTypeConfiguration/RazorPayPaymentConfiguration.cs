namespace kidsffw.Repository.EntityTypeConfiguration;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RazorPayPaymentConfiguration : IEntityTypeConfiguration<RazorPayPaymentEntity>
{
    public void Configure(EntityTypeBuilder<RazorPayPaymentEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AmountPaid).IsRequired();
        builder.Property(x => x.PaymentId).HasMaxLength(100).IsRequired();
        builder.Property(x => x.EventId).HasMaxLength(100).IsRequired();
        builder.Property(x => x.OrderId).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MobileNumber).HasMaxLength(12).IsRequired();
    }
}