using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using System;
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

            if (book is null)
                throw new NotFoundException($"Book {request.BookName} cannot be deleted because it does not exist.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
