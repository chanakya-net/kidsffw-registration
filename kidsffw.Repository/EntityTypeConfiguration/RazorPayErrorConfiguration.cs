namespace kidsffw.Repository.EntityTypeConfiguration;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RazorPayErrorConfiguration : IEntityTypeConfiguration<RazorPayErrorEntity>
{
    public void Configure(EntityTypeBuilder<RazorPayErrorEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MobileNumber).HasMaxLength(12).IsRequired();
        builder.Property(x => x.EventId).HasMaxLength(100).IsRequired();
        builder.Property(x => x.OrderId).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ErrorMessage).IsRequired();
    }
}