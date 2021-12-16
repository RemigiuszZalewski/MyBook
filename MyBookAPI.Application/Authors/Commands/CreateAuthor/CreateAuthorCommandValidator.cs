using FluentValidation;
using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;

namespace MyBookAPI.Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.Country).NotEmpty();
        }
    }
}
