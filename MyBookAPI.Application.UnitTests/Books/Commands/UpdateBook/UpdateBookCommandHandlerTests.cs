using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Commands.UpdateBook;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandlerTests : CommandBase
    {
        private readonly UpdateBookCommandHandler _handler;
        public UpdateBookCommandHandlerTests()
        {
            _handler = new UpdateBookCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_UpdateBookCommandCategoryDescription_BookUpdated()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Name = "Test Book",
                Category = "Romance",
                Description = "New description",
                Pages = 150,
                Price = 500,
                PublicationDate = DateTime.Now,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = await _handler.Handle(updateBookCommand, CancellationToken.None);

            //Assert
            var updatedBook = await _dbContext.Books.Where(x => x.Name.Equals("Test Book"))
                                                    .Include(x => x.Author)
                                                    .Include(x => x.Description)
                                                    .Include(x => x.Category)
                                                    .Include(x => x.PublishingHouse)
                                                    .FirstOrDefaultAsync();

            Assert.NotNull(updatedBook);
            Assert.Equal(updateBookCommand.AuthorFirstName, updatedBook.Author.AuthorName.FirstName);
            Assert.Equal(updateBookCommand.AuthorLastName, updatedBook.Author.AuthorName.LastName);
            Assert.Equal(updateBookCommand.Category, updatedBook.Category.Name);
            Assert.Equal(updateBookCommand.Name, updatedBook.Name);
            Assert.Equal(updateBookCommand.Description, updatedBook.Description.Text);
            Assert.Equal(updateBookCommand.PublicationDate, updatedBook.PublicationDate);
            Assert.Equal(updateBookCommand.Pages, updatedBook.Pages);
            Assert.Equal(updateBookCommand.Price, updatedBook.Price);
            Assert.Equal(updateBookCommand.PublishingHouse, updatedBook.PublishingHouse.Name);
        }

        [Fact]
        public async Task Handle_UpdateBookCommandBookDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Name = "I don't exist",
                Category = "Romance",
                Description = "New description",
                Pages = 150,
                Price = 500,
                PublicationDate = DateTime.Now,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(updateBookCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }

        [Fact]
        public async Task Handle_UpdateBookCommandOnlyNameOfBookSpecifiedNotningToBeUpdated_ArgumentExceptionRaised()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Name = "Test Book",
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(updateBookCommand, CancellationToken.None);

            //Assert
            result.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public async Task Handle_UpdateBookCommandOnlyDescriptionIsUpdatedRestOfTheFieldsHaveTheSameValues_BookUpdated()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                Name = "Test Book",
                Description = "New description",
            };

            var bookToBeUpdated = await _dbContext.Books.Where(x => x.Name.Equals("Test Book"))
                                                        .FirstOrDefaultAsync();

            //Act
            var result = await _handler.Handle(updateBookCommand, CancellationToken.None);

            //Assert
            var updatedBook = await _dbContext.Books.Where(x => x.Name.Equals("Test Book"))
                                                    .Include(x => x.Author)
                                                    .Include(x => x.Description)
                                                    .Include(x => x.Category)
                                                    .Include(x => x.PublishingHouse)
                                                    .FirstOrDefaultAsync();

            Assert.NotNull(updatedBook);
            Assert.Equal(bookToBeUpdated.Author.AuthorName.FirstName, updatedBook.Author.AuthorName.FirstName);
            Assert.Equal(bookToBeUpdated.Author.AuthorName.LastName, updatedBook.Author.AuthorName.LastName);
            Assert.Equal(bookToBeUpdated.Category.Name, updatedBook.Category.Name);
            Assert.Equal(updateBookCommand.Name, updatedBook.Name);
            Assert.Equal(updateBookCommand.Description, updatedBook.Description.Text);
            Assert.Equal(bookToBeUpdated.PublicationDate, updatedBook.PublicationDate);
            Assert.Equal(bookToBeUpdated.Pages, updatedBook.Pages);
            Assert.Equal(bookToBeUpdated.Price, updatedBook.Price);
            Assert.Equal(bookToBeUpdated.PublishingHouse.Name, updatedBook.PublishingHouse.Name);
        }
    }
}
