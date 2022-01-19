using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Reviews.Queries.GetReviews;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Reviews.Queries.GetReviews
{
    public class GetReviewsQueryHandlerTests : QueryBase
    {
        private readonly GetReviewsQueryHandler _handler;
        public GetReviewsQueryHandlerTests()
        {
            _handler = new GetReviewsQueryHandler(_dbContext, _mapper);
        }

        [Fact]
        public async Task Handle_GetReviewsQueryAllFieldsFilledIn_ListOfReviewsReturned()
        {
            //Arrange
            var getReviewsQuery = new GetReviewsQuery
            {
                BookName = "Test Book"
            };

            //Act
            var result = await _handler.Handle(getReviewsQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.BookReviews.Count() == 1);
        }

        [Fact]
        public async Task Handle_GetReviewsQueryBookWithoutReviews_EmptyListReturned()
        {
            //Arrange
            var getReviewsQuery = new GetReviewsQuery
            {
                BookName = "WithoutReviews"
            };

            //Act
            var result = await _handler.Handle(getReviewsQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.BookReviews.Count() == 0);
        }

        [Fact]
        public async Task Handle_GetReviewsQueryBookDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var getReviewsQuery = new GetReviewsQuery
            {
                BookName = "I don't exist"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(getReviewsQuery, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }
    }
}
