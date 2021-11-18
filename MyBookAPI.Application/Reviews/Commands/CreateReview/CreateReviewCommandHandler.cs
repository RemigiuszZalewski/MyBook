using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Reviews.Commands.CreateReview
{
    public class CreateBookReviewCommandHandler : IRequestHandler<CreateBookReviewCommand, int>
    {
        private readonly IMyBookDbContext _context;
        public CreateBookReviewCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBookReviewCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName)).FirstOrDefaultAsync(cancellationToken);
            var user = await _context.Users.Where(x => x.UserName.ToString().Equals(request.UserName)).FirstOrDefaultAsync(cancellationToken);

            Review review = new()
            {
                Text = request.Text,
                Stars = request.Stars,
                BookId = book.Id,
                UserId = user.Id
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync(cancellationToken);

            return review.Id;
        }
    }
}
