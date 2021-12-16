using FluentValidation;

namespace MyBookAPI.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookName).NotEmpty();
        }
    }
}
