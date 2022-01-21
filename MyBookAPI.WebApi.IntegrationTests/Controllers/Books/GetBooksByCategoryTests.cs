using MyBookAPI.Application.Books.Models;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class GetBooksByCategoryTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public GetBooksByCategoryTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetBooksByCategory_CategoryPassedIntoRequest_ListOfBooksReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();
            var category = "TestCategory";

            //Act
            var response = await client.GetAsync($"/api/books/category?category={category}");
            var booksVm = await Utilities.GetResponseContent<BooksVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(booksVm.Books.Count() == 2);
            Assert.True(booksVm.Books.Where(x => x.Category.Equals(category)).ToList().Count == 2);
        }
    }
}
