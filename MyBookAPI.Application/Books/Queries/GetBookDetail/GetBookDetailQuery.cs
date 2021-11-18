using MediatR;

namespace MyBookAPI.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQuery : IRequest<BookDetailVm>
    {
        public string BookName { get; set; }
    }
}
