using MyBookAPI.Application.Reviews.Commands.UpdateReview;
using MyBookAPI.WebApi.IntegrationTests.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Reviews
{
    public class UpdateReviewTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public UpdateReviewTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateReview_AllRequestedFieldsToBeUpdatedAreIncludedToRequest_ReviewUpdated()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Stars = 3,
                Text = "New review text"
            };

            var json = JsonConvert.SerializeObject(updateReviewCommand);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PatchAsync("/api/reviews", httpContent);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
