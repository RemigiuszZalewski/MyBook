using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IMyBookDbContext _context;
        public CreateBookCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals(request.AuthorFirstName) &&
                                                           x.AuthorName.LastName.Equals(request.AuthorLastName))
                                               .FirstOrDefaultAsync(cancellationToken);

            var publishingHouse = await _context.PublishingHouses.Where(x => x.Name.Equals(request.PublishingHouse))
                                                                 .FirstOrDefaultAsync(cancellationToken);

            var category = await _context.Categories.Where(x => x.Name.Equals(request.Category))
                                                    .FirstOrDefaultAsync(cancellationToken);

            if (author is null)
                author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals("Unknown"))
                                               .FirstOrDefaultAsync(cancellationToken);

            if (publishingHouse is null)
                publishingHouse = await _context.PublishingHouses.Where(x => x.Name.Equals("Unknown Publishing House"))
                                                                 .FirstOrDefaultAsync(cancellationToken);

            if (category is null)
                category = await _context.Categories.Where(x => x.Name.Equals("Unknown"))
                                                    .FirstOrDefaultAsync(cancellationToken);

            var book = new Book
            {
                Name = request.Name,
                Price = (request.Price ?? 0) > 0 ? request.Price : null,
                ToBeSold = request.Price != null ? true : false,
                Pages = request.Pages,
                CategoryId = category?.Id,
                PublicationDate = request.PublicationDate != null ? request.PublicationDate : null,
                PublishingHouseId = publishingHouse?.Id,
                AuthorId = author?.Id,
                Description = new()
                {
                    Text = request.Description
                }
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}
