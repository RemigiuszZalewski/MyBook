using MyBookAPI.Application.Books.Queries.GetBooksByCountry;
using MyBookAPI.Application.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBooksByCountry
{
    public class GetBooksByCountryQueryHandlerTests : QueryBase
    {
        private readonly GetBooksByCountryQueryHandler _handler;
        public GetBooksByCountryQueryHandlerTests()
        {
            _handler = new GetBooksByCountryQueryHandler(_dbContext, _mapper);
        }

        [Fact]
        public async Task Handle_GetBooksByCountryQueryNoBooksToCountryAssigned_EmptyListReturned()
        {
            //Arrange
            var getBookDetailQuery = new GetBooksByCountryQuery
            {
                Country = "Non-existing country"
            };

            //Act
            var result = await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Books.Count == 0);
        }

        [Fact]
        public async Task Handle_GetBooksByCountryQueryCountryHasBooksAssigned_ListOfBooksReturned()
        {
            //Arrange
            var getBookDetailQuery = new GetBooksByCountryQuery
            {
                Country = "Spain"
            };

            //Act
            var result = await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Books.Count == 2);
        }
    }
}
