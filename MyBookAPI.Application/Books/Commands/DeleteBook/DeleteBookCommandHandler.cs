using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IMyBookDbContext _context;
        public DeleteBookCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName)).FirstOrDefaultAsync(cancellationToken);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
