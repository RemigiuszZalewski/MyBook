using MediatR;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Common.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IMyBookDbContext _context;

        public CreateAuthorCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                AuthorName = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName
                },
                Description = new()
                {
                    Text = request.Description
                },
                Country = request.Country
            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author.Id;
        }
    }
}
