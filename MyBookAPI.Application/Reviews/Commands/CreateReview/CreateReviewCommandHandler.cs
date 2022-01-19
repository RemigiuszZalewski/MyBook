using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using MyBookAPI.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {
        private readonly IMyBookDbContext _context;
        public CreateReviewCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.Where(x => x.Name.Equals(request.BookName)).FirstOrDefaultAsync(cancellationToken);

            if (book == null)
                throw new NotFoundException($"Book with following name: {request.BookName} does not exist.");

            var user = _context.Users.AsEnumerable().Where(x => x.UserName.ToString() == request.UserName).FirstOrDefault();

            if (user == null)
                throw new NotFoundException($"User with the name: {request.UserName} does not exist.");

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
