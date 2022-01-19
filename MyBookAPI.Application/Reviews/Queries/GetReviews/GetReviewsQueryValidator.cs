using FluentValidation;

namespace MyBookAPI.Application.Reviews.Queries.GetReviews
{
    public class GetReviewsQueryValidator : AbstractValidator<GetReviewsQuery>
    {
        public GetReviewsQueryValidator()
        {
            RuleFor(x => x.BookName).NotEmpty();
        }
    }
}
