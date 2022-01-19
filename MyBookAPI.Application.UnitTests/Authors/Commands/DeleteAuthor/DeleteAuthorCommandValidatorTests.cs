using MyBookAPI.Application.Authors.Commands.DeleteAuthor;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        private readonly DeleteAuthorCommandValidator _validator;
        public DeleteAuthorCommandValidatorTests()
        {
            _validator = new DeleteAuthorCommandValidator();
        }

        [Fact]
        public void Validate_DeleteAuthorCommandWithAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var deleteAuthorCommand = new DeleteAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski"
            };

            //Act
            var result = _validator.Validate(deleteAuthorCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_DeleteAuthorCommandFirstNameEmpty_ValdiationFailed()
        {
            //Arrange
            var deleteAuthorCommand = new DeleteAuthorCommand
            {
                FirstName = "",
                LastName = "Dostojewski"
            };

            //Act
            var result = _validator.Validate(deleteAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("FirstName"));
        }

        [Fact]
        public void Validate_DeleteAuthorCommandLastNameEmpty_ValidationFailed()
        {
            //Arrange
            var deleteAuthorCommand = new DeleteAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = ""
            };

            //Act
            var result = _validator.Validate(deleteAuthorCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("LastName"));
        }
    }
}
