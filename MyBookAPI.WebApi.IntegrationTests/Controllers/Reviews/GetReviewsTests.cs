using MyBookAPI.Application.Reviews.Queries.GetReviews;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Reviews
{
    public class GetReviewsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public GetReviewsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetReviews_GetAllReviewsForSpecifiedBookInTheRequest_ListOfReviewsReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();
            var bookName = "Test Book";

            //Act
            var response = await client.GetAsync($"/api/reviews/{bookName}");
            var reviewsVm = await Utilities.GetResponseContent<ReviewsVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(reviewsVm.BookReviews.Count() > 0);
        }

        [Fact]
        public async Task GetReviews_BookWithoutAnyReviewsPassedIntoRequest_EmptyListOfReviewsReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();
            var bookName = "WithoutReviews";

            //Act
            var response = await client.GetAsync($"/api/reviews/{bookName}");
            var reviewsVm = await Utilities.GetResponseContent<ReviewsVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(reviewsVm.BookReviews.Count() == 0);
        }
    }
}
