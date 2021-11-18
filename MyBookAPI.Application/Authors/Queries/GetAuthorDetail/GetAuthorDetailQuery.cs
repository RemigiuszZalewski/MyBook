using MediatR;

namespace MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery : IRequest<AuthorDetailVm>
    {
        public string FullName { get; set; }
    }
}
