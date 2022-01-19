using FluentValidation;

namespace MyBookAPI.Application.Books.Queries.GetBooksByCategory
{
    public class GetBooksByCategoryQueryValidator : AbstractValidator<GetBooksByCategoryQuery>
    {
        public GetBooksByCategoryQueryValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
