using MyBookAPI.Application.Reviews.Queries.GetReviews;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Queries.GetReviews
{
    public class GetReviewsQueryValidatorTests
    {
        private readonly GetReviewsQueryValidator _validator;
        public GetReviewsQueryValidatorTests()
        {
            _validator = new GetReviewsQueryValidator();
        }

        [Fact]
        public void Validate_GetReviewsQueryBookNameFilledIn_ValidationSucceeded()
        {
            //Arrange
            var getReviewsQuery = new GetReviewsQuery
            {
                BookName = "Test Book"
            };

            //Act
            var result = _validator.Validate(getReviewsQuery);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_GetReviewsQueryBookNameNotFilledIn_ValidationFailed()
        {
            //Arrange
            var getReviewsQuery = new GetReviewsQuery
            {
                BookName = ""
            };

            //Act
            var result = _validator.Validate(getReviewsQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("BookName"));
        }
    }
}
