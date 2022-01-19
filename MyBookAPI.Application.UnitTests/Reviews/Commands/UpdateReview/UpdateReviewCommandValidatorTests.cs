using MyBookAPI.Application.Reviews.Commands.UpdateReview;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandValidatorTests
    {
        private readonly UpdateReviewCommandValidator _validator;
        public UpdateReviewCommandValidatorTests()
        {
            _validator = new UpdateReviewCommandValidator();
        }

        [Fact]
        public void Valdiate_UpdateReviewCommandAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Stars = 5,
                Text = "New review text."
            };

            //Act
            var result = _validator.Validate(updateReviewCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Valdiate_UpdateReviewCommandReviewIsNull_ValidationFailed()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                Stars = 5,
                Text = "New review text."
            };

            //Act
            var result = _validator.Validate(updateReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("ReviewId"));
        }

        [Fact]
        public void Valdiate_UpdateReviewCommandNumberOfStarsIsLessThan1_ValidationFailed()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Stars = 0,
                Text = "New review text."
            };

            //Act
            var result = _validator.Validate(updateReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Stars"));
        }

        [Fact]
        public void Valdiate_UpdateReviewCommandNumberOfStarsIsHigherThan5_ValidationFailed()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Stars = 10,
                Text = "New review text."
            };

            //Act
            var result = _validator.Validate(updateReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Stars"));
        }
    }
}
