using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Books.Models;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDetailVm>
    {
        private readonly IMyBookDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryHandler(IMyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BookDetailVm> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName))
                                           .Include(x => x.PublishingHouse)
                                           .Include(x => x.Category)
                                           .Include(x => x.Author)
                                           .FirstOrDefaultAsync(cancellationToken);

            if (book is null)
                throw new NotFoundException($"Book with the name: {request.BookName} has not been found.");

            var bookVm = _mapper.Map<BookDetailVm>(book);

            return bookVm;
        }
    }
}
