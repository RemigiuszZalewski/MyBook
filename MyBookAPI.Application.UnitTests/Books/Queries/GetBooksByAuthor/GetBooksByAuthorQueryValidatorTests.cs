using MyBookAPI.Application.Books.Queries.GetBooksByAuthor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBooksByAuthor
{
    public class GetBooksByAuthorQueryValidatorTests
    {
        private readonly GetBooksByAuthorQueryValidator _validator;
        public GetBooksByAuthorQueryValidatorTests()
        {
            _validator = new GetBooksByAuthorQueryValidator();
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryAuthorsFirstNameAndLastNameFilledIn_ValidationSucceeded()
        {
            //Arrange
            var getBooksByAuthorQuery = new GetBooksByAuthorQuery
            {
                FirstName = "Test",
                LastName = "Test"
            };

            //Act
            var result = _validator.Validate(getBooksByAuthorQuery);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryLackOfFirstName_ValidationFailed()
        {
            //Arrange
            var getBooksByAuthorQuery = new GetBooksByAuthorQuery
            {
                FirstName = "",
                LastName = "Test"
            };

            //Act
            var result = _validator.Validate(getBooksByAuthorQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("FirstName"));
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryLackOfLastName_ValidationFailed()
        {
            //Arrange
            var getBooksByAuthorQuery = new GetBooksByAuthorQuery
            {
                FirstName = "Test",
                LastName = ""
            };

            //Act
            var result = _validator.Validate(getBooksByAuthorQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("LastName"));
        }
    }
}
