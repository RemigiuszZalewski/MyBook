using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Persistance.Configurations
{
    public class PublishingHouseConfiguration : IEntityTypeConfiguration<PublishingHouse>
    {
        public void Configure(EntityTypeBuilder<PublishingHouse> builder)
        {
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.Address).Property(p => p.Country).HasMaxLength(25).HasColumnName("Country").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.City).HasMaxLength(25).HasColumnName("City").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.Street).HasMaxLength(30).HasColumnName("Street").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.ZipCode).HasMaxLength(6).HasColumnName("ZipCode").IsRequired();
            builder.OwnsOne(p => p.Description).Property(p => p.Text).HasMaxLength(2000).HasColumnName("Description");
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        }
    }
}
