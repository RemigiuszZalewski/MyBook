using MediatR;
using MyBookAPI.Application.Books.Models;

namespace MyBookAPI.Application.Books.Queries.GetBooksByAuthor
{
    public class GetBooksByAuthorQuery : IRequest<BooksVm>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
