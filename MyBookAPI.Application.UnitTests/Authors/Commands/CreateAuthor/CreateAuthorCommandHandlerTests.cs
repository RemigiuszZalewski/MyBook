using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;
using MyBookAPI.Application.UnitTests.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandlerTests : CommandBase
    {
        private readonly CreateAuthorCommandHandler _handler;
        public CreateAuthorCommandHandlerTests() : base()
        {
            _handler = new CreateAuthorCommandHandler(_dbContext);
        }

        [Fact]
        public async Task CreateAuthorCommandHandler_CreateAuthorWithProperValues_AuthorCreated()
        {
            //Arrange
            var author = new CreateAuthorCommand
            {
                FirstName = "Fiodor",
                LastName = "Dostojewski",
                Country = "Russia",
                Description = "Russian novelist"
            };

            //Act
            var result = await _handler.Handle(author, CancellationToken.None);

            //Assert
            var addedAuthor = await _dbContext.Authors.Where(x => x.Id == result).FirstOrDefaultAsync();

            Assert.NotNull(addedAuthor);
            Assert.Equal(addedAuthor.Id, result);
            Assert.Equal(author.FirstName, addedAuthor.AuthorName.FirstName);
            Assert.Equal(author.LastName, addedAuthor.AuthorName.LastName);
            Assert.Equal(author.Country, addedAuthor.Country);
            Assert.Equal(author.Description, addedAuthor.Description.Text);
        }
    }
}
