using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.UnitTests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryHandlerTests : QueryBase
    {
        private readonly GetAuthorDetailQueryHandler _handler;
        public GetAuthorDetailQueryHandlerTests() : base()
        {
            _handler = new GetAuthorDetailQueryHandler(_dbContext, _mapper);
        }

        [Fact]
        public async Task Handle_GetExistingAuthorByFirstNameAndLastName_AuthorFoundAndReturned()
        {
            //Arrange
            var getAuthorDetailQuery = new GetAuthorDetailQuery
            {
                FirstName = "John",
                LastName = "Test"
            };

            //Act
            var result = await _handler.Handle(getAuthorDetailQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.FullName, String.Join(" ", getAuthorDetailQuery.FirstName, getAuthorDetailQuery.LastName));
            Assert.NotNull(result.Books);
        }

        [Fact]
        public async Task Handle_GetAuthorDetailQueryAuthorDoesNotExist_NotFoundExceptionRaised()
        {
            //Arrange
            var getAuthorDetailQuery = new GetAuthorDetailQuery
            {
                FirstName = "John",
                LastName = "DoesNotExist"
            };

            //Act
            Func<Task> result = async () => await _handler.Handle(getAuthorDetailQuery, CancellationToken.None);

            //Assert
            result.ShouldThrow<NotFoundException>();
        }
    }
}
