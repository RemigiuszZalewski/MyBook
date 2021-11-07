using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.UserName).Property(p => p.FirstName).HasMaxLength(25).HasColumnName("FirstName").IsRequired();
            builder.OwnsOne(p => p.UserName).Property(p => p.LastName).HasMaxLength(25).HasColumnName("LastName").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.Country).HasMaxLength(25).HasColumnName("Country").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.City).HasMaxLength(25).HasColumnName("City").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.Street).HasMaxLength(30).HasColumnName("Street").IsRequired();
            builder.OwnsOne(p => p.Address).Property(p => p.ZipCode).HasMaxLength(6).HasColumnName("ZipCode").IsRequired();
            builder.Property(p => p.DateOfBirth).IsRequired();
        }
    }
}
