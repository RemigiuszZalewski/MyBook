using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Entities;
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
            var book = await _context.Books.Where(x => x.Name.Equals(request.Name)).FirstOrDefaultAsync(cancellationToken);

            var author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals(request.AuthorFirstName) &&
                                                           x.AuthorName.LastName.Equals(request.AuthorLastName)).FirstOrDefaultAsync(cancellationToken);

            var publishingHouse = await _context.PublishingHouses.Where(x => x.Name.Equals(request.PublishingHouse)).FirstOrDefaultAsync(cancellationToken);
            var category = await _context.Categories.Where(x => x.Name.Equals(request.Category)).FirstOrDefaultAsync(cancellationToken);

            book.Price = (request.Price ?? 0) > 0 ? request.Price : null;
            book.Description = request.Description != null ? new Description { Text = request.Description } : book.Description;
            book.ToBeSold = request.Price != null ? true : false;
            book.CategoryId = category != null ? category.Id : book.CategoryId;
            book.PublicationDate = request.PublicationDate != null ? request.PublicationDate : null;
            book.PublishingHouseId = publishingHouse != null ? publishingHouse.Id : book.PublishingHouseId;
            book.AuthorId = author?.Id;
            book.Pages = request.Pages != null ? (int) request.Pages : book.Pages;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
