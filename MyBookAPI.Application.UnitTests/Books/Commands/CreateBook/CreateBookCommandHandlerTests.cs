using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Commands.CreateBook;
using MyBookAPI.Application.UnitTests.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Commands.CreateBook
{
    public class CreateBookCommandHandlerTests : CommandBase
    {
        private readonly CreateBookCommandHandler _handler;
        public CreateBookCommandHandlerTests()
        {
            _handler = new CreateBookCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_AllNessesaryFieldsFilledIn_BookCreated()
        {
            //Arrange
            var command = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "TestBook123",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _handler.Handle(command, CancellationToken.None);

            //Assert
            var createdBook = await _dbContext.Books.Where(x => x.Name.Equals(command.Name))
                                                    .Include(x => x.Author)
                                                    .Include(x => x.Category)
                                                    .Include(x => x.Description)
                                                    .Include(x => x.PublishingHouse)
                                                    .FirstOrDefaultAsync();

            Assert.NotNull(createdBook);
            Assert.Equal(command.AuthorFirstName, createdBook.Author.AuthorName.FirstName);
            Assert.Equal(command.AuthorLastName, createdBook.Author.AuthorName.LastName);
            Assert.Equal(command.Category, createdBook.Category.Name);
            Assert.Equal(command.Name, createdBook.Name);
            Assert.Equal(command.Description, createdBook.Description.Text);
            Assert.Equal(command.PublicationDate, createdBook.PublicationDate);
            Assert.Equal(command.Pages, createdBook.Pages);
            Assert.Equal(command.Price, createdBook.Price);
            Assert.Equal(command.PublishingHouse, createdBook.PublishingHouse.Name);
        }

        [Fact]
        public async Task Handle_LackOfCategoryPublishingHouseAndAuthor_BookCreatedWithUnknownValuesOfThoseFields()
        {
            //Arrange
            var command = new CreateBookCommand
            {
                AuthorFirstName = "",
                AuthorLastName = "",
                Category = "",
                Name = "TestBook123",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = ""
            };

            var firstUnknownValue = "Unknown";
            var secondUnknownValue = "Author";
            var thirdUnknownValue = "Unknown Publishing House";

            //Act
            var result = _handler.Handle(command, CancellationToken.None);

            //Assert
            var createdBook = await _dbContext.Books.Where(x => x.Name.Equals(command.Name))
                                                    .Include(x => x.Author)
                                                    .Include(x => x.Category)
                                                    .Include(x => x.Description)
                                                    .Include(x => x.PublishingHouse)
                                                    .FirstOrDefaultAsync();

            Assert.NotNull(createdBook);
            Assert.Equal(createdBook.Author.AuthorName.FirstName, firstUnknownValue);
            Assert.Equal(createdBook.Author.AuthorName.LastName, secondUnknownValue);
            Assert.Equal(createdBook.Category.Name, firstUnknownValue);
            Assert.Equal(command.Name, createdBook.Name);
            Assert.Equal(command.Description, createdBook.Description.Text);
            Assert.Equal(command.PublicationDate, createdBook.PublicationDate);
            Assert.Equal(command.Pages, createdBook.Pages);
            Assert.Equal(command.Price, createdBook.Price);
            Assert.Equal(createdBook.PublishingHouse.Name, thirdUnknownValue);
        }
    }
}
