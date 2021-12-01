using MediatR;

namespace MyBookAPI.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<int>
    {
        public string Text { get; set; }
        public int Stars { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; }
    }
}
