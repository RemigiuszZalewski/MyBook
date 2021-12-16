using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, AuthorDetailVm>
    {
        private readonly IMyBookDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryHandler(IMyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AuthorDetailVm> Handle(GetAuthorDetailQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.Where(x => x.AuthorName.FirstName.Equals(request.FirstName) &&
                                                           x.AuthorName.LastName.Equals(request.LastName))
                                                .Include(x => x.Books)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (author is null)
                throw new NotFoundException($"Author with the name: {request.FirstName} {request.LastName} has not been found.");

            var authorVm = _mapper.Map<AuthorDetailVm>(author);

            return authorVm;
        }
    }
}
