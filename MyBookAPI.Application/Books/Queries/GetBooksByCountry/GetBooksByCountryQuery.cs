using MediatR;
using MyBookAPI.Application.Books.Models;

namespace MyBookAPI.Application.Books.Queries.GetBooksByCountry
{
    public class GetBooksByCountryQuery : IRequest<BooksVm>
    {
        public string Country { get; set; }
    }
}
