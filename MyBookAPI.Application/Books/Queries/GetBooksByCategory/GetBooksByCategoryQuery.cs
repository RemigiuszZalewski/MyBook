using MediatR;
using MyBookAPI.Application.Books.Models;

namespace MyBookAPI.Application.Books.Queries.GetBooksByCategory
{
    public class GetBooksByCategoryQuery : IRequest<BooksVm>
    {
        public string Category { get; set; }
    }
}
