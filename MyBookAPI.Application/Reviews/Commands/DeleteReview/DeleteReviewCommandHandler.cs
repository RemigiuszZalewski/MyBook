using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly IMyBookDbContext _context;
        public DeleteReviewCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.Where(x => x.Id == request.ReviewId).FirstOrDefaultAsync(cancellationToken);

            if (review is null)
                throw new NotFoundException($"Review cannot be deleted because it does not exist.");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
