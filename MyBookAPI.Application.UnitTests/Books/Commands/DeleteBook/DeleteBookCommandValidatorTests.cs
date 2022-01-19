using MyBookAPI.Application.Books.Commands.DeleteBook;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        private readonly DeleteBookCommandValidator _validator;
        public DeleteBookCommandValidatorTests()
        {
            _validator = new DeleteBookCommandValidator();
        }

        [Fact]
        public void Validate_DeleteBookCommandWithBookNameFilledIn_ValidationSucceeded()
        {
            //Arrange
            var deleteBookCommand = new DeleteBookCommand
            {
                BookName = "Book Name"
            };

            //Act
            var result = _validator.Validate(deleteBookCommand);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_DeleteBookCommandBookNameEmpty_ValidationFailed()
        {
            //Arrange
            var deleteBookCommand = new DeleteBookCommand
            {
                BookName = ""
            };

            //Act
            var result = _validator.Validate(deleteBookCommand);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("BookName"));
        }
    }
}
