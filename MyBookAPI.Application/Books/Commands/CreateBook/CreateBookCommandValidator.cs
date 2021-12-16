using FluentValidation;

namespace MyBookAPI.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Price).LessThan(1000);
            RuleFor(x => x.Pages).InclusiveBetween(10, 2000);
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.PublishingHouse).NotEmpty();
            RuleFor(x => x.AuthorFirstName).NotEmpty();
            RuleFor(x => x.AuthorLastName).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(2000).NotEmpty();
        }
    }
}
