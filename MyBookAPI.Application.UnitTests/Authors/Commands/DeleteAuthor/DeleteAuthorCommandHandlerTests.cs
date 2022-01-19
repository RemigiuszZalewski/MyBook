using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Authors.Commands.DeleteAuthor;
using MyBookAPI.Application.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandlerTests : CommandBase
    {
        private readonly DeleteAuthorCommandHandler _handler;
        public DeleteAuthorCommandHandlerTests() : base()
        {
            _handler = new DeleteAuthorCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_DeleteAuthorCommandWithProperId_AuthorSuccessfullyDeleted()
        {
            //Arrange
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            var deleteAuthorCommand = new DeleteAuthorCommand
            {
                FirstName = author.AuthorName.FirstName,
                LastName = author.AuthorName.LastName
            };

            //Act
            var result = await _handler.Handle(deleteAuthorCommand, CancellationToken.None);

            //Assert
            var authorAfterRemoval = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));
            Assert.Null(authorAfterRemoval);
        }
    }
}
