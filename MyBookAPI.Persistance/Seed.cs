using Microsoft.EntityFrameworkCore;
using MyBookAPI.Domain.Entities;
using System;

namespace MyBookAPI.Persistance
{
    public static class Seed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(r =>
            {
                r.HasData(new Author
                {
                    Id = 1,
                    StatusId = 1,
                    Created = DateTime.Now
                });
                r.OwnsOne(r => r.AuthorName).HasData(new
                {
                    AuthorId = 1,
                    FirstName = "Adam",
                    LastName = "Mickiewicz"
                });
            });

            modelBuilder.Entity<PublishingHouse>(r =>
            {
                r.HasData(new PublishingHouse
                {
                    Id = 1,
                    StatusId = 1,
                    Created = DateTime.Now,
                    Name = "BestPublishingHouse"
                });
                r.OwnsOne(r => r.Address).HasData(new
                {
                    PublishingHouseId = 1,
                    Country = "Poland",
                    City = "Warsaw",
                    Street = "Kolorowa 3/2",
                    ZipCode = "00-014"
                });
            });

            modelBuilder.Entity<Category>(r =>
            {
                r.HasData(new Category { Id = 1, Name = "Fantasy and Science Fiction" },
                          new Category { Id = 2, Name = "Horror" },
                          new Category { Id = 3, Name = "Thriller" },
                          new Category { Id = 4, Name = "History" },
                          new Category { Id = 5, Name = "Poetry" },
                          new Category { Id = 6, Name = "Crime" },
                          new Category { Id = 7, Name = "Travel" },
                          new Category { Id = 8, Name = "Drama" },
                          new Category { Id = 9, Name = "Biography" },
                          new Category { Id = 10, Name = "Science" },
                          new Category { Id = 11, Name = "Cookbook" },
                          new Category { Id = 12, Name = "Health and Diet" },
                          new Category { Id = 13, Name = "Classic" },
                          new Category { Id = 14, Name = "Romance" });
            });

            modelBuilder.Entity<Book>(r =>
            {
                r.HasData(new Book { AuthorId = 1, PublishingHouseId = 1, Id = 1, CategoryId = 5, Name = "Pan Tadeusz" },
                          new Book { AuthorId = 1, PublishingHouseId = 1, Id = 2, CategoryId = 8, Name = "Dziady II" },
                          new Book { AuthorId = 1, PublishingHouseId = 1, Id = 3, CategoryId = 8, Name = "Konrad Wallenrod" });
            });
        }
    }
}
