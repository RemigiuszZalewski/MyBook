using MyBookAPI.Application.Authors.Commands.UpdateAuthor;
using MyBookAPI.WebApi.IntegrationTests.Common;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Authors
{
    public class UpdateAuthorTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public UpdateAuthorTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateAuthor_AllRequestedFieldsToBeUpdatedAreIncludedToRequest_AuthorUpdated()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "John",
                LastName = "Test",
                Description = "New description"
            };

            var json = JsonConvert.SerializeObject(updateAuthorCommand);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PatchAsync("/api/authors", httpContent);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
