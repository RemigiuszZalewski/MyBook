using FluentValidation;

namespace MyBookAPI.Application.Books.Queries.GetBooksByAuthor
{
    public class GetBooksByAuthorQueryValidator : AbstractValidator<GetBooksByAuthorQuery>
    {
        public GetBooksByAuthorQueryValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
