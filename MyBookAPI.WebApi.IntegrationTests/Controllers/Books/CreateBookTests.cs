using MyBookAPI.Application.Books.Commands.CreateBook;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class CreateBookTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public CreateBookTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateBook_AllNessessaryFieldsInRequestFilledIn_BookCreated()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            //Act
            var response = await client.PostAsJsonAsync("/api/books", new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "TestBook123",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            });

            //Assert
            Assert.True((int)response.StatusCode == 201);
        }
    }
}
