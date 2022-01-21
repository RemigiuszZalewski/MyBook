using MyBookAPI.Application.Books.Commands.UpdateBook;
using MyBookAPI.WebApi.IntegrationTests.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class UpdateAuthorTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public UpdateAuthorTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateBook_AllRequestedFieldsToBeUpdatedAreIncludedToRequest_BookUpdated()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Name = "Test Book",
                Category = "Romance",
                Description = "New description",
                Pages = 150,
                Price = 500,
                PublicationDate = DateTime.Now,
                PublishingHouse = "TestPublishingHouse"
            };

            var json = JsonConvert.SerializeObject(updateBookCommand);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PatchAsync("/api/books", httpContent);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
