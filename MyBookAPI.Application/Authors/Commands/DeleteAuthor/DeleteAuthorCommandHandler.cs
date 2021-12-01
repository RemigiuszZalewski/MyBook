using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IMyBookDbContext _context;
        public DeleteAuthorCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals(request.FirstName) &&
                                                           x.AuthorName.LastName.Equals(request.LastName)).FirstOrDefaultAsync(cancellationToken);

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
