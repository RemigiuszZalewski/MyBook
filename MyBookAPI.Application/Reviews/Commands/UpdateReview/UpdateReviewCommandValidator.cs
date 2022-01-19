using FluentValidation;

namespace MyBookAPI.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId).NotEqual(0);
            RuleFor(x => x.Text).MaximumLength(4000);
            RuleFor(x => x.Stars).InclusiveBetween(1, 5);
        }
    }
}
