using MyBookAPI.Application.Reviews.Commands.DeleteReview;
using MyBookAPI.WebApi.IntegrationTests.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Reviews
{
    public class DeleteReviewTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public DeleteReviewTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeleteReview_AllFieldsPassedIntoRequest_ReviewDeleted()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var deleteReviewCommand = new DeleteReviewCommand
            {
                ReviewId = 1,
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost/api/reviews"),
                Content = new StringContent(JsonConvert.SerializeObject(deleteReviewCommand), Encoding.UTF8, "application/json")
            };

            //Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.True((int)response.StatusCode == 204);
        }
    }
}
