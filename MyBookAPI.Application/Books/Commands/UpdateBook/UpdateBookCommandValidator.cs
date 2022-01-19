using FluentValidation;

namespace MyBookAPI.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Price).LessThan(1000);
            RuleFor(x => x.Pages).InclusiveBetween(10, 2000);
            RuleFor(x => x.Description).MaximumLength(2000);
        }
    }
}
