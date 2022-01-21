using MyBookAPI.Application.Books.Models;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class GetBooksByAuthorTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public GetBooksByAuthorTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetBooksByAuthor_AuthorPassedIntoRequest_ListOfBooksReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var firstName = "John";
            var lastName = "Test";

            //Act
            var response = await client.GetAsync($"/api/books/author?firstName={firstName}&lastName={lastName}");
            var booksVm = await Utilities.GetResponseContent<BooksVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(booksVm.Books.Count() > 0);
        }

        [Fact]
        public async Task GetBooksByAuthor_AuthorWithoutAnyBooksPassedIntoRequest_EmptyListOfBooksReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();

            var firstName = "I don't";
            var lastName = "Have books";

            //Act
            var response = await client.GetAsync($"/api/books/author?firstName={firstName}&lastName={lastName}");
            var booksVm = await Utilities.GetResponseContent<BooksVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(booksVm.Books.Count() == 0);
        }
    }
}
