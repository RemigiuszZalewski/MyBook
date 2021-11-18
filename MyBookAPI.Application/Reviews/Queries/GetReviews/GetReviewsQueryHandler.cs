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
        public GetReviewsQueryHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<ReviewsVm> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews.Where(x => x.Book.Name == request.BookName).ToListAsync();

            var reviewsDtoList = new List<ReviewDto>();

            reviews.ForEach(c =>
            {
                reviewsDtoList.Add(new ReviewDto
                {
                    UserName = c.User.UserName.ToString(),
                    Stars = c.Stars,
                    Text = c.Text
                });
            });

            return new ReviewsVm
            {
                BookReviews = reviewsDtoList
            };
        }
    }
}
