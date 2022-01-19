using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Authors.Commands.UpdateAuthor;
using MyBookAPI.Application.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.Application.UnitTests.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandlerTests : CommandBase
    {
        private readonly UpdateAuthorCommandHandler _handler;
        public UpdateAuthorCommandHandlerTests()
        {
            _handler = new UpdateAuthorCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_UpdateCountryAndDescription_SuccessfulUpdateOfExistingAuthor()
        {
            //Arrange
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "John",
                LastName = "Test",
                Country = "Nikaragua",
                Description = "Changed description"
            };

            //Act
            var result = await _handler.Handle(updateAuthorCommand, CancellationToken.None);

            //Assert
            var updatedAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            Assert.Equal(author.AuthorName.FirstName, updatedAuthor.AuthorName.FirstName);
            Assert.Equal(author.AuthorName.LastName, updatedAuthor.AuthorName.LastName);
            Assert.Equal(updatedAuthor.Country, updateAuthorCommand.Country);
            Assert.Equal(updatedAuthor.Description.Text, updateAuthorCommand.Description);
        }

        [Fact]
        public async Task Handle_UpdateOnlyCountry_SuccessfulUpdateOfExistingAuthor()
        {
            //Arrange
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "John",
                LastName = "Test",
                Country = "Nikaragua",
            };

            //Act
            var result = await _handler.Handle(updateAuthorCommand, CancellationToken.None);

            //Assert
            var updatedAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            Assert.Equal(author.AuthorName.FirstName, updatedAuthor.AuthorName.FirstName);
            Assert.Equal(author.AuthorName.LastName, updatedAuthor.AuthorName.LastName);
            Assert.Equal(updatedAuthor.Country, updateAuthorCommand.Country);
            Assert.Equal(updatedAuthor.Description.Text, author.Description.Text);
        }

        [Fact]
        public async Task Handle_NoValuesToUpdate_AuthorLeftWithAllPreviouslyExistingValues()
        {
            //Arrange
            var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            var updateAuthorCommand = new UpdateAuthorCommand
            {
                FirstName = "John",
                LastName = "Test",
                Country = "Nikaragua",
            };

            //Act
            var result = await _handler.Handle(updateAuthorCommand, CancellationToken.None);

            //Assert
            var updatedAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.AuthorName.LastName.Equals("Test"));

            Assert.Equal(author.AuthorName.FirstName, updatedAuthor.AuthorName.FirstName);
            Assert.Equal(author.AuthorName.LastName, updatedAuthor.AuthorName.LastName);
            Assert.Equal(author.Country, updatedAuthor.Country);
            Assert.Equal(author.Description.Text, updatedAuthor.Description.Text);
        }
    }
}
