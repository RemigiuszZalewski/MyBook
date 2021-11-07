using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Persistance.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Pages).HasMaxLength(2000);
            builder.Property(p => p.Price).HasPrecision(2,2);
            builder.Property(p => p.ReleaseYear).HasDefaultValue(2000);
            builder.Property(p => p.ToBeSold).HasDefaultValue(true);
        }
    }
}
