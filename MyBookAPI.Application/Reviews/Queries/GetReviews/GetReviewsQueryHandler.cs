using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Reviews.Queries.GetReviews
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, ReviewsVm>
    {
        private readonly IMyBookDbContext _context;
        private readonly IMapper _mapper;
        public GetReviewsQueryHandler(IMyBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ReviewsVm> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews.Where(x => x.Book.Name == request.BookName).ToListAsync();
            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);

            return new ReviewsVm
            {
                BookReviews = reviewsDto
            };
        }
    }
}
