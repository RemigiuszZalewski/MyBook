using MyBookAPI.Application.Reviews.Commands.CreateReview;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Reviews
{
    public class CreateReviewTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public CreateReviewTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateReview_AllNessesaryInformationPassedIntoRequest_ReviewCreated()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            //Act
            var response = await client.PostAsJsonAsync("api/reviews", new CreateReviewCommand
            {
                BookName = "Test Book",
                Stars = 5,
                Text = "Amazing book!!!",
                UserName = "Alberto Torres"
            });

            //Assert
            Assert.True((int)response.StatusCode == 201);
        }
    }
}
