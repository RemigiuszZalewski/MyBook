using MediatR;

namespace MyBookAPI.Application.Reviews.Queries.GetReviews
{
    public class GetReviewsQuery : IRequest<ReviewsVm>
    {
        public string BookName { get; set; }
    }
}
