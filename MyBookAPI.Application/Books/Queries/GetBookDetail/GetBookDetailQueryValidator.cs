using FluentValidation;

namespace MyBookAPI.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(x => x.BookName).NotEmpty();
        }
    }
}
