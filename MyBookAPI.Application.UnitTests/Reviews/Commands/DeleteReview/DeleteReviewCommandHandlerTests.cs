using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Reviews.Commands.DeleteReview;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandlerTests : CommandBase
    {
        private readonly DeleteReviewCommandHandler _handler;
        public DeleteReviewCommandHandlerTests()
        {
            _handler = new DeleteReviewCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_DeleteReviewCommandAllFieldsFilledIn_ReviewDeleted()
        {
            //Arrange
            var deleteReviewCommand = new DeleteReviewCommand
            {
                ReviewId = 1
            };

            //Act
            var result = await _handler.Handle(deleteReviewCommand, CancellationToken.None);

            //Assert
            var deletedReview = await _dbContext.Reviews.Where(x => x.Id == deleteReviewCommand.ReviewId)
                                                        .FirstOrDefaultAsync();

            Assert.Null(deletedReview);
        }

        [Fact]
        public async Task Handle_DeleteReviewCommandReviewDoesNotExist_ReviewDeleted()
        {
            //Arrange
            var deleteReviewCommand = new DeleteReviewCommand
            {
                ReviewId = 500
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(deleteReviewCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }
    }
}
