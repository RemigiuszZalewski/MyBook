using MediatR;

namespace MyBookAPI.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
    }
}
