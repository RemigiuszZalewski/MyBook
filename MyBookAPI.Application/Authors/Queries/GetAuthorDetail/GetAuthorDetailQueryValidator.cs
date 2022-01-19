using FluentValidation;
using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;

namespace MyBookAPI.Application.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
