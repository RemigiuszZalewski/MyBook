using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName)).FirstOrDefaultAsync(cancellationToken);
            var bookVm = _mapper.Map<BookDetailVm>(book);

            return bookVm;
        }
    }
}
