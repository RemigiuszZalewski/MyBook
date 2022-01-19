using MyBookAPI.Application.Books.Queries.GetBooksByCountry;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBooksByCountry
{
    public class GetBooksByCountryQueryValidatorTests
    {
        private readonly GetBooksByCountryQueryValidator _validator;
        public GetBooksByCountryQueryValidatorTests()
        {
            _validator = new GetBooksByCountryQueryValidator();
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryCategoryFilledIn_ValidationSucceeded()
        {
            //Arrange
            var getBooksByCountryQuery = new GetBooksByCountryQuery
            {
                Country = "Test Country"
            };

            //Act
            var result = _validator.Validate(getBooksByCountryQuery);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryLackOfCategory_ValidationFailed()
        {
            //Arrange
            var getBooksByCountryQuery = new GetBooksByCountryQuery
            {
                Country = ""
            };

            //Act
            var result = _validator.Validate(getBooksByCountryQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Country"));
        }
    }
}
