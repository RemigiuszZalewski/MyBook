using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Models;
using MyBookAPI.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Queries.GetBooksByCountry
{
    public class GetBooksByCountryQueryHandler : IRequestHandler<GetBooksByCountryQuery, BooksVm>
    {
        private readonly IMyBookDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksByCountryQueryHandler(IMyBookDbContext context, IMapper mapper)
        {
            _context = context;
        }
        public async Task<BooksVm> Handle(GetBooksByCountryQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.Where(x => x.Author.Country.Equals(request.Country)).ToListAsync(cancellationToken);
            var booksDto = _mapper.Map<List<BookDto>>(books);

            return new BooksVm
            {
                Books = booksDto
            };
        }
    }
}
