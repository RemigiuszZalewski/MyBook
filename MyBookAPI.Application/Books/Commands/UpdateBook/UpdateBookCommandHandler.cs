using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IMyBookDbContext _context;
        public UpdateBookCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Where(x => x.Name.Equals(request.Name) ).FirstOrDefaultAsync(cancellationToken);
            var author = await _context.Authors.Where(x => x.AuthorName.ToString().Equals(request.AuthorName)).FirstOrDefaultAsync(cancellationToken);
            var publishingHouse = await _context.PublishingHouses.Where(x => x.Name.Equals(request.PublishingHouse)).FirstOrDefaultAsync(cancellationToken);
            var category = await _context.Categories.Where(x => x.Name.Equals(request.Category)).FirstOrDefaultAsync(cancellationToken);

            book.Price = request.Price;
            book.Description = new()
            {
                Text = request.Description
            };
            book.ToBeSold = request.Price != null ? true : false;
            book.CategoryId = category != null ? category.Id : null;
            book.PublicationDate = request.PublicationDate;
            book.PublishingHouseId = publishingHouse != null ? publishingHouse.Id : null;
            book.AuthorId = author != null ? author.Id : null;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
