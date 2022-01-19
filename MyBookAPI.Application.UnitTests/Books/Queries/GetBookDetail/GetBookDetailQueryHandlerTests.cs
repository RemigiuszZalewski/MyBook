using MyBookAPI.Application.Books.Queries.GetBookDetail;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryHandlerTests : QueryBase
    {
        private readonly GetBookDetailQueryHandler _handler;
        public GetBookDetailQueryHandlerTests()
        {
            _handler = new GetBookDetailQueryHandler(_dbContext, _mapper);
        }

        [Fact]
        public async Task Handle_GetBookDetailAllFieldsFilledIn_BookFoundAndDetailsReturned()
        {
            //Arrange
            var getBookDetailQuery = new GetBookDetailQuery
            {
                BookName = "Test Book"
            };

            //Act
            var result = await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Author);
            Assert.Equal(getBookDetailQuery.BookName, result.Name);
            Assert.NotNull(result.Category);
            Assert.NotNull(result.Description);
            Assert.NotNull(result.PublishingHouse);
            Assert.NotNull(result.Price);
            Assert.True(result.ToBeSold);
        }

        [Fact]
        public async Task Handle_GetBookDetailBookDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var getBookDetailQuery = new GetBookDetailQuery
            {
                BookName = "I don't exist"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(getBookDetailQuery, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }
    }
}
