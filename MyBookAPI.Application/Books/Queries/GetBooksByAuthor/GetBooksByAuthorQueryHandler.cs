using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Models;
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
            var books = await _context.Books.Where(x => x.Author.AuthorName.ToString().Equals(request.AuthorName)).ToListAsync(cancellationToken);
            var booksDto = _mapper.Map<List<BookDto>>(books);

            return new BooksVm
            {
                Books = booksDto
            };
        }
    }
}
