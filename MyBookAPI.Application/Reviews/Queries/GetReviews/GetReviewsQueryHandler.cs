using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
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
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName)).FirstOrDefaultAsync();

            if (book is null)
                throw new NotFoundException($"Book with the name: {request.BookName} does not exist");

            var reviews = await _context.Reviews.Where(x => x.Book.Name.Equals(request.BookName))
                                                .Include(x => x.User)
                                                .ToListAsync(cancellationToken);

            if (reviews.Count() == 0)
                return new ReviewsVm { BookReviews = new List<ReviewDto>() };

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);

            return new ReviewsVm
            {
                BookReviews = reviewsDto
            };
        }
    }
}
