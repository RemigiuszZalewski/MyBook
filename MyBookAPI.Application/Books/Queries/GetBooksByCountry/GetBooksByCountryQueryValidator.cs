using FluentValidation;

namespace MyBookAPI.Application.Books.Queries.GetBooksByCountry
{
    public class GetBooksByCountryQueryValidator : AbstractValidator<GetBooksByCountryQuery>
    {
        public GetBooksByCountryQueryValidator()
        {
            RuleFor(x => x.Country).NotEmpty();
        }
    }
}
