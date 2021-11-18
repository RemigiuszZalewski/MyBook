using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDetailVm>
    {
        private readonly IMyBookDbContext _context;
        public GetBookDetailQueryHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<BookDetailVm> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName)).FirstOrDefaultAsync(cancellationToken);

            var reviews = new List<ReviewDto>();

            if (book.Reviews.Count > 0)
            {
                foreach (var review in book.Reviews)
                {
                    reviews.Add(new ReviewDto
                    {
                        Stars = review.Stars,
                        Text = review.Text
                    });
                }
            }

            return new BookDetailVm
            {
                Name = book.Name,
                Price = book.Price,
                ToBeSold = book.ToBeSold,
                Pages = book.Pages,
                Category = book.Category.Name,
                PublishingHouse = book.PublishingHouse.Name,
                Author = book.Author.AuthorName.ToString(),
                Description = book.Description.Text,
                Reviews = reviews
            };
        }
    }
}
