using MediatR;

namespace MyBookAPI.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest
    {
        public string FullName { get; set; }
    }
}
