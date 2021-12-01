using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IMyBookDbContext _context;
        public UpdateAuthorCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals(request.FirstName) &&
                                                           x.AuthorName.LastName.Equals(request.LastName)).FirstOrDefaultAsync(cancellationToken);

            author.Country = request.Country;
            author.Description.Text = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
