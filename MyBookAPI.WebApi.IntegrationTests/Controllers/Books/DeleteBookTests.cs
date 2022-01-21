using MyBookAPI.Application.Books.Commands.DeleteBook;
using MyBookAPI.WebApi.IntegrationTests.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class DeleteBookTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public DeleteBookTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeleteBook_BookNamePassedIntoRequest_BookDeleted()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var deleteBookCommand = new DeleteBookCommand
            {
                BookName = "Test Book"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost/api/books"),
                Content = new StringContent(JsonConvert.SerializeObject(deleteBookCommand), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.True((int)response.StatusCode == 204);
        }
    }
}
