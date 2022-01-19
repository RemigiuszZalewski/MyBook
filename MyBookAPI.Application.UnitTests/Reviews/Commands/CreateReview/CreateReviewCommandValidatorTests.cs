using MyBookAPI.Application.Reviews.Commands.CreateReview;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidatorTests
    {
        private readonly CreateReviewCommandValidator _validator;
        public CreateReviewCommandValidatorTests()
        {
            _validator = new CreateReviewCommandValidator();
        }

        [Fact]
        public void Validate_CreateReviewCommandAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Stars = 5,
                Text = "Amazing Book",
                UserName = "Alberto Torres"
            };

            //Act
            var result = _validator.Validate(createReviewCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_CreateReviewCommandStarsMoreThan5_ValidationFailed()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Stars = 10,
                Text = "Amazing Book",
                UserName = "Alberto Torres"
            };

            //Act
            var result = _validator.Validate(createReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Stars"));
        }

        [Fact]
        public void Validate_CreateReviewCommandStarsLessThan1_ValidationFailed()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Stars = 0,
                Text = "Amazing Book",
                UserName = "Alberto Torres"
            };

            //Act
            var result = _validator.Validate(createReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Stars"));
        }

        [Fact]
        public void Validate_CreateReviewCommandStarsAreNull_ValidationFailed()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Text = "Amazing Book",
                UserName = "Alberto Torres"
            };

            //Act
            var result = _validator.Validate(createReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Stars"));
        }

        [Fact]
        public void Validate_CreateReviewCommandLackOfBookName_ValidationFailed()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "",
                Text = "Amazing Book",
                Stars = 4,
                UserName = "Alberto Torres"
            };

            //Act
            var result = _validator.Validate(createReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("BookName"));
        }

        [Fact]
        public void Validate_CreateReviewCommandLackOfText_ValidationFailed()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Text = "",
                Stars = 4,
                UserName = "Alberto Torres"
            };

            //Act
            var result = _validator.Validate(createReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Text"));
        }
    }
}
