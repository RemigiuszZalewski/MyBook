using MyBookAPI.Application.Authors.Queries.GetAuthorDetail;
using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;
using System.Linq;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests
    {
        private readonly GetAuthorDetailQueryValidator _validator;
        public GetAuthorDetailQueryValidatorTests()
        {
            _validator = new GetAuthorDetailQueryValidator();
        }

        [Fact]
        public void Validate_GetAuthorDetailQueryAllFieldsFilledIn_ValidationSucceeded()
        {
            //Arrange
            var getAuthorDetailQuery = new GetAuthorDetailQuery
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski"
            };

            //Act
            var result = _validator.Validate(getAuthorDetailQuery);

            //Assert
            Assert.True(result.IsValid);
            Assert.False(result.Errors.Any());
        }

        [Fact]
        public void Validate_GetAuthorDetailQueryFirstNameEmpty_ValidationFailed()
        {
            //Arrange
            var getAuthorDetailQuery = new GetAuthorDetailQuery
            {
                FirstName = "",
                LastName = "Dostojewski"
            };

            //Act
            var result = _validator.Validate(getAuthorDetailQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("FirstName"));
        }

        [Fact]
        public void Validate_GetAuthorDetailQueryLastNameEmpty_ValidationFailed()
        {
            //Arrange
            var getAuthorDetailQuery = new GetAuthorDetailQuery
            {
                FirstName = "Fiodor",
                LastName = ""
            };

            //Act
            var result = _validator.Validate(getAuthorDetailQuery);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName.Equals("LastName"));
        }
    }
}
