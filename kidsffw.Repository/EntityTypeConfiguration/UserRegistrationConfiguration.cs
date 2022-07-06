namespace kidsffw.Repository.EntityTypeConfiguration;

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserRegistrationConfiguration : IEntityTypeConfiguration<UserRegistrationEntity>
{
    public void Configure(EntityTypeBuilder<UserRegistrationEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MobileNumber).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Age).HasMaxLength(2).IsRequired();
        builder.Property(x => x.KidName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.City).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Gender).HasMaxLength(10).IsRequired();
        builder.Property(x => x.CouponCode).HasMaxLength(6).IsRequired(false);
        builder.Property(x => x.ParentName).HasMaxLength(100).IsRequired();
        // builder.Property(x => x.OtpVerified).IsRequired(false);

    }
}