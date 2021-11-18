using MediatR;

namespace MyBookAPI.Application.Common.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
    }
}
