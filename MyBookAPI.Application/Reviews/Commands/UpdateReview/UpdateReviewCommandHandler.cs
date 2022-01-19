using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBookAPI.Application.Common.Exceptions;
using MyBookAPI.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookAPI.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly IMyBookDbContext _context;
        public UpdateReviewCommandHandler(IMyBookDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.Where(x => x.Id == request.ReviewId).FirstOrDefaultAsync(cancellationToken);

            if (review is null)
                throw new NotFoundException($"There's no review to be updated.");

            review.Stars = request.Stars != null ? (int) request.Stars : review.Stars;
            review.Text = request.Text != null ? request.Text : review.Text;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
