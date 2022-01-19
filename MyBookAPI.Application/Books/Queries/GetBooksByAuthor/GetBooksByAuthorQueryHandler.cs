using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Models;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Queries.GetBooksByAuthor
{
    public class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksByAuthorQuery, BooksVm>
    {
        private readonly IMyBookDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksByAuthorQueryHandler(IMyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BooksVm> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals(request.FirstName) &&
                                                      x.AuthorName.LastName.Equals(request.LastName))
                                               .FirstOrDefaultAsync(cancellationToken);

            if (author is null)
                throw new NotFoundException($"Author: {request.FirstName} {request.LastName} does not exist.");

            var books = await _context.Books.Where(x => x.Author.AuthorName.FirstName.Equals(request.FirstName) &&
                                                        x.Author.AuthorName.LastName.Equals(request.LastName))
                                            .Include(x => x.PublishingHouse)
                                            .Include(x => x.Category)
                                            .Include(x => x.Author)
                                            .ToListAsync(cancellationToken);

            if (books.Count == 0)
                return new BooksVm { Books = new List<BookDetailVm>() };

            var bookDetailVms = _mapper.Map<List<BookDetailVm>>(books);

            return new BooksVm { Books = bookDetailVms };
        }
    }
}
