using MediatR;

namespace MyBookAPI.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest
    {
        public string BookName { get; set; }
    }
}
