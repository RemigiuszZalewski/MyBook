using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Persistance.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Stars).HasMaxLength(5).IsRequired();
            builder.Property(p => p.Text).HasMaxLength(2000).IsRequired();
        }
    }
}
