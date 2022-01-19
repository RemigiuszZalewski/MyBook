using MyBookAPI.Application.Books.Queries.GetBooksByCategory;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBooksByCategory
{
    public class GetBooksByCategoryQueryValidatorTests
    {
        private readonly GetBooksByCategoryQueryValidator _validator;
        public GetBooksByCategoryQueryValidatorTests()
        {
            _validator = new GetBooksByCategoryQueryValidator();
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryCategoryFilledIn_ValidationSucceeded()
        {
            //Arrange
            var getBooksByCategoryQuery = new GetBooksByCategoryQuery
            {
                Category = "TestCategory"
            };

            //Act
            var result = _validator.Validate(getBooksByCategoryQuery);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_GetBooksByAuthorQueryLackOfCategory_ValidationFailed()
        {
            //Arrange
            var getBooksByCategoryQuery = new GetBooksByCategoryQuery
            {
                Category = ""
            };

            //Act
            var result = _validator.Validate(getBooksByCategoryQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("Category"));
        }
    }
}
