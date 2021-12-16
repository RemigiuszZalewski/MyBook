using FluentValidation;

namespace MyBookAPI.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Text).MaximumLength(4000);
            RuleFor(x => x.Stars).InclusiveBetween(1, 5);
            RuleFor(x => x.BookName).NotNull();
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
