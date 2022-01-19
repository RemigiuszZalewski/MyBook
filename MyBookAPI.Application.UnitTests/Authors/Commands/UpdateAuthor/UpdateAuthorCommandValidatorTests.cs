using MyBookAPI.Application.Authors.Commands.UpdateAuthor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests
    {
        private readonly UpdateAuthorCommandValidator _validator;

        public UpdateAuthorCommandValidatorTests()
        {
            _validator = new UpdateAuthorCommandValidator();
        }

        [Fact]
        public void Validate_UpdateAuthorCommandWithAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(updateAuthorCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_UpdateAuthorCommandWithOnlyFirstNameAndLastNameFilledIn_ValidationSucceeded()
        {
            //Arrange
            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski",
            };

            //Act
            var result = _validator.Validate(updateAuthorCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_UpdateAuthorCommandFirstNameEmpty_ValidationFailed()
        {
            //Arrange
            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "",
                LastName = "Dostojewski",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(updateAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("FirstName"));
        }

        [Fact]
        public void Validate_UpdateAuthorCommandLastNameEmpty_ValidationFailed()
        {
            //Arrange
            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = _validator.Validate(updateAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("LastName"));
        }
    }
}
