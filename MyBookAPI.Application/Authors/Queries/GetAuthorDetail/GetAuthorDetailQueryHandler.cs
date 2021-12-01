using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                                                           x.AuthorName.LastName.Equals(request.LastName)).FirstOrDefaultAsync(cancellationToken);
            var authorVm = _mapper.Map<AuthorDetailVm>(author);

            return authorVm;
        }
    }
}
