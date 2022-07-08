namespace kidsffw.Repository.EntityTypeConfiguration;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OtpConfiguration : IEntityTypeConfiguration<OtpEntity>
{
    public void Configure(EntityTypeBuilder<OtpEntity> builder)
    {
        builder.ToTable("otps");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Otp).HasMaxLength(6).IsRequired();
        builder.Property(x => x.ValidTill).IsRequired();
        builder.Property(x => x.MobileNumber).HasMaxLength(10).IsRequired();
    }
}