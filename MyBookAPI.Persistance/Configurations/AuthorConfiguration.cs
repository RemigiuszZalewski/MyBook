using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBookAPI.Domain.Entities;

namespace MyBookAPI.Persistance.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.AuthorName).Property(p => p.FirstName).HasMaxLength(20).HasColumnName("FirstName").IsRequired();
            builder.OwnsOne(p => p.AuthorName).Property(p => p.LastName).HasMaxLength(20).HasColumnName("LastName").IsRequired();
            builder.OwnsOne(p => p.Description).Property(p => p.Text).HasMaxLength(2000).HasColumnName("Description");
        }
    }
}
