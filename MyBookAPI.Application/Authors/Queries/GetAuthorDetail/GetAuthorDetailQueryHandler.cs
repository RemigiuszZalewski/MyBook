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
        public GetAuthorDetailQueryHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<AuthorDetailVm> Handle(GetAuthorDetailQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.Where(x => x.AuthorName.ToString().Equals(request.FullName)).FirstOrDefaultAsync(cancellationToken);
            
            return new AuthorDetailVm
            {
                Description = author.Description.Text,
                Books = author.Books.ToList()
            };
        }
    }
}
