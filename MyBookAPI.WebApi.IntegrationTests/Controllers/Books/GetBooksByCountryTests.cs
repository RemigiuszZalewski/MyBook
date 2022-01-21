using MyBookAPI.Application.Books.Models;
using MyBookAPI.WebApi.IntegrationTests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyBookAPI.WebApi.IntegrationTests.Controllers.Books
{
    public class GetBooksByCountryTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public GetBooksByCountryTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetBooksByCountry_CountryPassedIntoRequest_ListOfBooksReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();
            var country = "Spain";

            //Act
            var response = await client.GetAsync($"/api/books/country?country={country}");
            var booksVm = await Utilities.GetResponseContent<BooksVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(booksVm.Books.Count() == 2);
        }

        [Fact]
        public async Task GetBooksByCountry_CountryWithoutBooksPassedIntoRequest_EmptyListOfBooksReturned()
        {
            //Arrange
            var client = await _factory.GetAuthenticatedClientAsync();
            var country = "Egypt";

            //Act
            var response = await client.GetAsync($"/api/books/country?country={country}");
            var booksVm = await Utilities.GetResponseContent<BooksVm>(response);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(booksVm.Books.Count() == 0);
        }
    }
}
