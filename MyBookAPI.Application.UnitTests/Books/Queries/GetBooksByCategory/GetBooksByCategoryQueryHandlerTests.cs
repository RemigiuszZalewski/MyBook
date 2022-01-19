using MyBookAPI.Application.Books.Queries.GetBooksByCategory;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBooksByCategory
{
    public class GetBooksByCategoryQueryHandlerTests : QueryBase
    {
        private readonly GetBooksByCategoryQueryHandler _handler;
        public GetBooksByCategoryQueryHandlerTests()
        {
            _handler = new GetBooksByCategoryQueryHandler(_dbContext, _mapper);
        }

        [Fact]
        public async Task Handle_GetBooksByAuthorQueryCategoryDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var getBookDetailQuery = new GetBooksByCategoryQuery
            {
                Category = "Non-existing category"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }

        [Fact]
        public async Task Handle_GetBooksByAuthorQueryAuthorDoesntHaveAnyBooks_EmptyListReturned()
        {
            //Arrange
            var getBookDetailQuery = new GetBooksByCategoryQuery
            {
                Category = "WithoutBooks"
            };

            //Act
            var result = await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Books.Count == 0);
        }

        [Fact]
        public async Task Handle_GetBooksByAuthorQueryCategoryHasBooksAssigned_ListOfBooksReturned()
        {
            //Arrange
            var getBookDetailQuery = new GetBooksByCategoryQuery
            {
                Category = "TestCategory"
            };

            //Act
            var result = await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Books.Count == 2);
        }
    }
}
