using MyBookAPI.Domain.Entities;
using MyBookAPI.Persistance;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBookAPI.WebApi.IntegrationTests.Common
{
    public class Utilities
    {
        public static void InitializeDatabase(MyBookDbContext dbContext)
        {
            var author = new Author
            {
                Id = 3,
                Country = "Spain",
                Description = new Description { Text = "This is test author" },
                AuthorName = new PersonName { FirstName = "John", LastName = "Test" },
            };

            var authorWithoutBooks = new Author
            {
                Id = 4,
                Country = "Poland",
                Description = new Description { Text = "I don't have books." },
                AuthorName = new PersonName { FirstName = "I don't", LastName = "Have books" }
            };
            dbContext.Authors.AddRange(author, authorWithoutBooks);

            var category = new Category
            {
                Id = 16,
                Name = "TestCategory"
            };

            var categoryWithNoBooksAssigned = new Category
            {
                Id = 17,
                Name = "WithoutBooks"
            };
            dbContext.Categories.AddRange(category, categoryWithNoBooksAssigned);

            var publishingHouse = new PublishingHouse
            {
                Id = 3,
                Name = "TestPublishingHouse",
                Address = new Address
                {
                    City = "Warsaw",
                    Country = "Poland",
                    Street = "TestStreet",
                    ZipCode = "00-231"
                },
                Description = new Description { Text = "This is test publishing house" }
            };
            dbContext.PublishingHouses.Add(publishingHouse);

            var book = new Book
            {
                Id = 4,
                AuthorId = 3,
                CategoryId = 16,
                Name = "Test Book",
                Description = new Description { Text = "This is test book" },
                PublicationDate = DateTime.Now,
                Pages = 150,
                Price = 51,
                PublishingHouseId = 3,
                ToBeSold = true
            };

            var bookWithoutReviews = new Book
            {
                Id = 5,
                AuthorId = 3,
                CategoryId = 16,
                Name = "WithoutReviews",
                Description = new Description { Text = "This is test book" },
                PublicationDate = DateTime.Now,
                Pages = 150,
                Price = 51,
                PublishingHouseId = 3,
                ToBeSold = true
            };
            dbContext.Books.AddRange(book, bookWithoutReviews);

            var user = new User
            {
                Id = 1,
                UserName = new PersonName { FirstName = "Alberto", LastName = "Torres" },
                DateOfBirth = DateTime.Now,
                Description = new Description { Text = "Alberto from Spain." },
                Address = new Address
                {
                    City = "Madrid",
                    Country = "Spain",
                    Street = "Calle de perro amarillo 44",
                    ZipCode = "00-212"
                }
            };
            dbContext.Users.Add(user);

            var review = new Review
            {
                Id = 1,
                BookId = 4,
                Stars = 1,
                Text = "This book is boring!",
                UserId = 1
            };
            dbContext.Reviews.Add(review);

            dbContext.SaveChanges();
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }
    }
}
