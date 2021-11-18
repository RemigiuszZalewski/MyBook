using System.Collections.Generic;

namespace MyBookAPI.Application.Reviews.Queries.GetReviews
{
    public class ReviewsVm
    {
        public ICollection<ReviewDto> BookReviews { get; set; }
    }
}
