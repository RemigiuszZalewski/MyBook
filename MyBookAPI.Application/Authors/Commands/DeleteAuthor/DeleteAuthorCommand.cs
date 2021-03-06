using MediatR;

namespace MyBookAPI.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
