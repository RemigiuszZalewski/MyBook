using MyBookAPI.Application.Authors.Commands.DeleteAuthor;
using MyBookAPI.WebApi.IntegrationTests.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Authors
{
    public class DeleteAuthorTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public DeleteAuthorTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeleteAuthor_DeleteAuthorThatHasBeenAddedIntoDatabaseByTheFactory_AuthorDeleted()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var deleteAuthorCommand = new DeleteAuthorCommand
            {
                FirstName = "John",
                LastName = "Test"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost/api/authors"),
                Content = new StringContent(JsonConvert.SerializeObject(deleteAuthorCommand), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.True((int)response.StatusCode == 204);
        }
    }
}
