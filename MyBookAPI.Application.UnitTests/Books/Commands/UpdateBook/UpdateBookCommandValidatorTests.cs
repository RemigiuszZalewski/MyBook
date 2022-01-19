using MyBookAPI.Application.Books.Commands.UpdateBook;
using System;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        private readonly UpdateBookCommandValidator _validator;
        public UpdateBookCommandValidatorTests()
        {
            _validator = new UpdateBookCommandValidator();
        }

        [Fact]
        public void Validate_UpdateBookCommandNameLongerThan100Characters_ValidationFailed()
        {
            //Arrange
            var name = "11111111112222222222333333333344444444445555555555" +
                "666666666677777777778888888888999999999900000000000000000";

            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = name,
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(updateBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Name"));
        }

        [Fact]
        public void Validate_UpdateBookCommandPagesLessThan10_ValidationFailed()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "Test Book",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 9,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(updateBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Pages"));
        }

        [Fact]
        public void Validate_UpdateBookCommandPagesMoreThan2000_ValidationFailed()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "Test Book",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 2001,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(updateBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Pages"));
        }

        [Fact]
        public void Validate_UpdateBookCommandPriceIs_ValidationHigherThan1000_ValidationFailed()
        {
            //Arrange
            var updateBookCommand = new UpdateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "Test Book",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 1001,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(updateBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Price"));
        }
    }
}
