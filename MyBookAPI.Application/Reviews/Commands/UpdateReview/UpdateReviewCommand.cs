using MediatR;

namespace MyBookAPI.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommand : IRequest
    {
        public int ReviewId { get; set; }
        public int? Stars { get; set; }
        public string Text { get; set; }
    }
}
