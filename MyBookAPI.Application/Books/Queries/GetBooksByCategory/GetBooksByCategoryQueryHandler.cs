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

namespace MyBookAPI.Application.Books.Queries.GetBooksByCategory
{
    public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, BooksVm>
    {
        private readonly IMyBookDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksByCategoryQueryHandler(IMyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BooksVm> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.Where(x => x.Category.Name.Equals(request.Category))
                                            .Include(x => x.Author)
                                            .Include(x => x.Category)
                                            .Include(x => x.PublishingHouse)
                                            .ToListAsync(cancellationToken);

            if (books is null || books.Count == 0)
                throw new NotFoundException($"No books from category {request.Category} have been found.");

            var bookDetailVms = _mapper.Map<List<BookDetailVm>>(books);

            return new BooksVm { Books = bookDetailVms };
        }
    }
}
