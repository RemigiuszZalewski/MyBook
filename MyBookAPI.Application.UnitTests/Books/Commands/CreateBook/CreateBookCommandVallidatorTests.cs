using MyBookAPI.Application.Books.Commands.CreateBook;
using System;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Commands.CreateBook
{
    public class CreateBookCommandVallidatorTests
    {
        private readonly CreateBookCommandValidator _validator;
        public CreateBookCommandVallidatorTests()
        {
            _validator = new CreateBookCommandValidator();
        }

        [Fact]
        public void Validate_CreateBookCommandAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "Test Book",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_CreateBookCommandLackOfAuthorCategoryAndPublishingHouse_ValidationSucceeded()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "",
                AuthorLastName = "",
                Category = "",
                Name = "Test Book",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = ""
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_CreateBookCommandNameLongerThan100Characters_ValidationFailed()
        {
            //Arrange
            var name = "11111111112222222222333333333344444444445555555555" +
                "666666666677777777778888888888999999999900000000000000000";

            var createBookCommand = new CreateBookCommand
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
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Name"));
        }

        [Fact]
        public void Validate_CreateBookCommandPriceHigherThan1000_ValidationFailed()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "Test Book",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 2000,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Price"));
        }

        [Fact]
        public void Validate_CreateBookCommandNameEmpty_ValidationFailed()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Name"));
        }

        [Fact]
        public void Validate_CreateBookCommandPagesLessThan10_ValidationFailed()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 9,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Pages"));
        }

        [Fact]
        public void Validate_CreateBookCommandPagesMoreThan2000_ValidationFailed()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "",
                Description = "Book to be tested",
                PublicationDate = DateTime.Now,
                Pages = 2001,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Pages"));
        }

        [Fact]
        public void Validate_CreateBookCommandDescriptionEmpty_ValidationFailed()
        {
            //Arrange
            var createBookCommand = new CreateBookCommand
            {
                AuthorFirstName = "John",
                AuthorLastName = "Test",
                Category = "TestCategory",
                Name = "Test Book",
                Description = "",
                PublicationDate = DateTime.Now,
                Pages = 400,
                Price = 10,
                PublishingHouse = "TestPublishingHouse"
            };

            //Act
            var result = _validator.Validate(createBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Description"));
        }
    }
}
