using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Reviews.Commands.CreateReview;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandlerTests : CommandBase
    {
        private readonly CreateReviewCommandHandler _handler;
        public CreateReviewCommandHandlerTests()
        {
            _handler = new CreateReviewCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_CreateReviewCommandBookExistsAllFieldsFilledIn_ReviewCreated()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Stars = 5,
                Text = "Amazing book",
                UserName = "Alberto Torres"
            };

            //Act
            var result = await _handler.Handle(createReviewCommand, CancellationToken.None);

            //Assert
            var book = await _dbContext.Books.Where(x => x.Name.Equals("Test Book")).FirstOrDefaultAsync();

            var addedReview = book.Reviews.Where(x => x.Text.Equals(createReviewCommand.Text) 
                                && x.User.UserName.ToString().Equals(createReviewCommand.UserName)).FirstOrDefault();

            Assert.NotNull(addedReview);
            Assert.Equal(createReviewCommand.BookName, book.Name);
            Assert.Equal(createReviewCommand.Stars, addedReview.Stars);
            Assert.Equal(createReviewCommand.Text, addedReview.Text);
            Assert.Equal(createReviewCommand.UserName, addedReview.User.UserName.ToString());
        }

        [Fact]
        public async Task Handle_CreateReviewCommandBookDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "I don't exist",
                Stars = 5,
                Text = "Amazing book",
                UserName = "Alberto Torres"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(createReviewCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }

        [Fact]
        public async Task Handle_CreateReviewCommandUserDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var createReviewCommand = new CreateReviewCommand
            {
                BookName = "Test Book",
                Stars = 5,
                Text = "Amazing book",
                UserName = "Nonexisting User"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(createReviewCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }
    }
}
