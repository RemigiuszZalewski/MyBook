using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Authors
{
    public class CreateAuthorTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public CreateAuthorTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateAuthor_AllNessesaryFieldsFilledIn_AuthorCreated()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            //Act
            var response = await client.PostAsJsonAsync("api/authors", new CreateAuthorCommand
            {
                FirstName = "Adam",
                LastName = "Jackson",
                Country = "USA",
                Description = "Integration test author"
            });

            //Assert
            Assert.True((int)response.StatusCode == 201);
        }
    }
}
