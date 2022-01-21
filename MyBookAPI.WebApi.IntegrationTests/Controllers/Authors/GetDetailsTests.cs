using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Authors
{
    public class GetDetailsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public GetDetailsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task GetDetails_FirstNameAndLastNameOfAuthorPassedToRequest_AuthorReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var firstName = "John";
            var lastName = "Test";

            //Act
            var response = await client.GetAsync($"api/authors?firstName={firstName}&lastName={lastName}");
            response.EnsureSuccessStatusCode();
            var authorDetailVm = await Utilities.GetResponseContent<AuthorDetailVm>(response);

            //Assert
            Assert.NotNull(authorDetailVm);
            Assert.True(authorDetailVm.Books.Count() == 2);
            Assert.Equal(authorDetailVm.FullName, string.Join(" ", firstName, lastName));
        }
    }
}
