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
            var author = await _context.Authors.Where(x => x.AuthorName.ToString().Equals(request.AuthorName)).FirstOrDefaultAsync(cancellationToken);
            var publishingHouse = await _context.PublishingHouses.Where(x => x.Name.Equals(request.PublishingHouse)).FirstOrDefaultAsync(cancellationToken);
            var category = await _context.Categories.Where(x => x.Name.Equals(request.Category)).FirstOrDefaultAsync(cancellationToken);

            var book = new Book
            {
                Name = request.Name,
                Price = request.Price,
                ToBeSold = request.Price != null ? true : false,
                Pages = request.Pages,
                CategoryId = category != null ? category.Id : null,
                PublicationDate = request.PublicationDate,
                PublishingHouseId = publishingHouse != null ? publishingHouse.Id : null,
                AuthorId = author != null ? author.Id : null,
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
