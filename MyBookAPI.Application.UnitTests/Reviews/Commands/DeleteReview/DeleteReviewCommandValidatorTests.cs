using MyBookAPI.Application.Reviews.Commands.DeleteReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandValidatorTests
    {
        private readonly DeleteReviewCommandValidator _validator;
        public DeleteReviewCommandValidatorTests()
        {
            _validator = new DeleteReviewCommandValidator();
        }

        [Fact]
        public void Validate_DeleteReviewCommandReviewIdFilledIn_ValidationSucceeded()
        {
            //Arrange
            var deleteReviewCommand = new DeleteReviewCommand
            {
                ReviewId = 1
            };

            //Act
            var result = _validator.Validate(deleteReviewCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_DeleteReviewCommandReviewIdNotFilledIn_ValidationFailed()
        {
            //Arrange
            var deleteReviewCommand = new DeleteReviewCommand
            {
                
            };

            //Act
            var result = _validator.Validate(deleteReviewCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("ReviewId"));
        }
    }
}
