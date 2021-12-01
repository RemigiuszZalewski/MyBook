using MediatR;
using MyBookAPI.Application.Books.Models;

namespace MyBookAPI.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQuery : IRequest<BookDetailVm>
    {
        public string BookName { get; set; }
    }
}
