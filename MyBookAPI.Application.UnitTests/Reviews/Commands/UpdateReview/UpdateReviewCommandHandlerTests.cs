using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Reviews.Commands.UpdateReview;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandlerTests : CommandBase
    {
        private readonly UpdateReviewCommandHandler _handler;
        public UpdateReviewCommandHandlerTests()
        {
            _handler = new UpdateReviewCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_UpdateReviewCommand_X()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Stars = 3,
                Text = "New review text"
            };

            //Act
            var result = _handler.Handle(updateReviewCommand, CancellationToken.None);

            //Assert
            var updatedReview = await _dbContext.Reviews.Where(x => x.Id == updateReviewCommand.ReviewId).FirstOrDefaultAsync();

            Assert.Equal(updatedReview.Id, updateReviewCommand.ReviewId);
            Assert.Equal(updatedReview.Text, updateReviewCommand.Text);
            Assert.Equal(updatedReview.Stars, updateReviewCommand.Stars);
        }

        [Fact]
        public async Task Handle_UpdateReviewCommandReviewToBeUpdatedNotFound_NotFoundExceptionRaised()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 500,
                Stars = 3,
                Text = "New review text"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(updateReviewCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }

        [Fact]
        public async Task Handle_UpdateReviewCommandReviewNoStartsInRequestValueIsSameWithValue5_ReviewUpdated()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Text = "New review text"
            };

            var stars = 1;
            var text = "This book is boring!";

            //Act
            var result = _handler.Handle(updateReviewCommand, CancellationToken.None);

            //Assert
            var updatedReview = await _dbContext.Reviews.Where(x => x.Id == updateReviewCommand.ReviewId).FirstOrDefaultAsync();

            Assert.Equal(updatedReview.Id, updateReviewCommand.ReviewId);
            Assert.Equal(updatedReview.Text, updateReviewCommand.Text);
            Assert.NotEqual(updatedReview.Text, text);
            Assert.Equal(updatedReview.Stars, stars);
        }

        [Fact]
        public async Task Handle_UpdateReviewCommandNoTextInRequestOnlyStarsToBeUpdatedTextIsSameLikeBefore_ReviewUpdated()
        {
            //Arrange
            var updateReviewCommand = new UpdateReviewCommand
            {
                ReviewId = 1,
                Stars = 3,
            };

            var stars = 1;

            //Act
            var result = _handler.Handle(updateReviewCommand, CancellationToken.None);

            //Assert
            var updatedReview = await _dbContext.Reviews.Where(x => x.Id == updateReviewCommand.ReviewId)
                                                        .FirstOrDefaultAsync();

            Assert.Equal(updatedReview.Id, updateReviewCommand.ReviewId);
            Assert.NotEmpty(updatedReview.Text);
            Assert.NotEqual(updatedReview.Stars, stars);
            Assert.Equal(updatedReview.Stars, updateReviewCommand.Stars);
        }
    }
}
