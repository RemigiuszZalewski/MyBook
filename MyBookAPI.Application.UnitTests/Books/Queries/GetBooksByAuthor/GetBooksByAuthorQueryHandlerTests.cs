using MyBookAPI.Application.Books.Queries.GetBooksByAuthor;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBooksByAuthor
{
    public class GetBooksByAuthorQueryHandlerTests : QueryBase
    {
        private readonly GetBooksByAuthorQueryHandler _handler;
        public GetBooksByAuthorQueryHandlerTests()
        {
            _handler = new GetBooksByAuthorQueryHandler(_dbContext, _mapper);
        }

        [Fact]
        public async Task Handle_GetBooksByAuthorQueryAuthorDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var getBookDetailQuery = new GetBooksByAuthorQuery
            {
                FirstName = "I don't",
                LastName = "Exist"
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
            var getBookDetailQuery = new GetBooksByAuthorQuery
            {
                FirstName = "I don't",
                LastName = "Have books"
            };

            //Act
            var result = await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Books.Count == 0);
        }

        [Fact]
        public async Task Handle_GetBooksByAuthorQueryWithProperAuthorName_ListOfBooksReturned()
        {
            //Arrange
            var getBooksByAuthorQuery = new GetBooksByAuthorQuery
            {
                FirstName = "John",
                LastName = "Test"
            };

            //Act
            var result = await _handler.Handle(getBooksByAuthorQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Books.Count > 0);
        }
    }
}
