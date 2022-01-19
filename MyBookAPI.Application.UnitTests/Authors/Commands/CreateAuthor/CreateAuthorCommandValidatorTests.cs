using MyBookAPI.Application.Authors.Commands.CreateAuthor;
using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        private readonly CreateAuthorCommandValidator _validator;
        public CreateAuthorCommandValidatorTests()
        {
            _validator = new CreateAuthorCommandValidator();
        }

        [Fact]
        public void Validate_CreateAuthorCommandWithAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var createAuthorCommand = new CreateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(createAuthorCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_CreateAuthorCommandFirstNameEmpty_ValidationFailed()
        {
            //Arrange
            var createAuthorCommand = new CreateAuthorCommand
            {
                FirstName = "",
                LastName = "Dostojewski",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(createAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "FirstName");
        }

        [Fact]
        public void Validate_CreateAuthorCommandLastNameEmpty_ValidationFailed()
        {
            //Arrange
            var createAuthorCommand = new CreateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(createAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "LastName");
        }

        [Fact]
        public void Validate_CreateAuthorCommandCountryEmpty_ValidationFailed()
        {
            //Arrange
            var createAuthorCommand = new CreateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski",
                Country = "",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(createAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "Country");
        }

        [Fact]
        public void Validate_CreateAuthorCommandDescriptionEmpty_ValidationSucceeded()
        {
            //Arrange
            var createAuthorCommand = new CreateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski",
                Country = "Russia",
                Description = ""
            };

            //Act
            var result = _validator.Validate(createAuthorCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }
    }
}
