using FluentValidation;

namespace MyBookAPI.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId).NotNull();
        }
    }
}
