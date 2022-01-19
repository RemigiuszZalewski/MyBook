using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Commands.DeleteBook;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandlerTests : CommandBase
    {
        private readonly DeleteBookCommandHandler _handler;
        public DeleteBookCommandHandlerTests()
        {
            _handler = new DeleteBookCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_DeleteExistingBookWithAllNessesaryFieldsFilledIn_BookIsInactivated()
        {
            //Arrange
            var deleteBookCommand = new DeleteBookCommand
            {
                BookName = "Test Book"
            };

            var user = "test@test.com";

            //Act
            var result = await _handler.Handle(deleteBookCommand, CancellationToken.None);

            //Assert
            var deletedBook = await _dbContext.Books.Where(x => x.Name.Equals(deleteBookCommand.BookName))
                                              .FirstOrDefaultAsync();

            Assert.Equal(deletedBook.InactivatedBy, user);
        }

        [Fact]
        public async Task Handle_RequestedBookToBeDeletedDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var deleteBookCommand = new DeleteBookCommand
            {
                BookName = "I don't exist"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(deleteBookCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }
    }
}
