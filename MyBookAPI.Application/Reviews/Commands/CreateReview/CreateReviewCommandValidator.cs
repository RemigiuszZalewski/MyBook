using FluentValidation;

namespace MyBookAPI.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Text).MaximumLength(4000).NotEmpty();
            RuleFor(x => x.Stars).InclusiveBetween(1, 5).NotNull();
            RuleFor(x => x.BookName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
