using MyBookAPI.Application.Books.Queries.GetBookDetail;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        private readonly GetBookDetailQueryValidator _validator;
        public GetBookDetailQueryValidatorTests()
        {
            _validator = new GetBookDetailQueryValidator();
        }

        [Fact]
        public void Validate_GetBookDetailQueryWithBookName_ValidationSucceeded()
        {
            //Arrange
            var getBookDetailQuery = new GetBookDetailQuery
            {
                BookName = "Test Name"
            };

            //Act
            var result = _validator.Validate(getBookDetailQuery);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_GetBookDetailQueryBookNameEmpty_ValidationFailed()
        {
            //Arrange
            var getBookDetailQuery = new GetBookDetailQuery
            {
                BookName = ""
            };

            //Act
            var result = _validator.Validate(getBookDetailQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("BookName"));
        }
    }
}
