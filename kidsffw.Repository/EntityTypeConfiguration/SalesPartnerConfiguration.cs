namespace kidsffw.Repository.EntityTypeConfiguration;

using kidsffw.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SalesPartnerConfiguration : IEntityTypeConfiguration<SalesPartnerEntity>
{
    public void Configure(EntityTypeBuilder<SalesPartnerEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ContactNumber).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}
