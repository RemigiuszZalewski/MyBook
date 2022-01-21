using MyBookAPI.Application.Books.Models;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class GetDetailsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public GetDetailsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetDetails_BookNamePassedIntoTheRequest_BookDetailsReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();
            var bookName = "Test Book";
            var author = "John Test";

            //Act
            var response = await client.GetAsync($"/api/books/details?bookName={bookName}");
            var bookVm = await Utilities.GetResponseContent<BookDetailVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(bookName, bookVm.Name);
            Assert.NotEmpty(bookVm.Category);
            Assert.NotEmpty(bookVm.Description);
            Assert.Equal(bookVm.Author, author);
        }
    }
}
