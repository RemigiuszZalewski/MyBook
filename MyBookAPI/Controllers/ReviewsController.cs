using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Reviews.Commands.CreateReview;
using MyBookAPI.Application.Reviews.Commands.DeleteReview;
using MyBookAPI.Application.Reviews.Commands.UpdateReview;
using MyBookAPI.Application.Reviews.Queries.GetReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ReviewsVm>> GetReviews([FromQuery] string bookName)
        {
            var vm = await Mediator.Send(new GetReviewsQuery { BookName = bookName });
            return vm;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateReview([FromQuery] string bookName, [FromQuery] int stars, 
                                                          [FromQuery] string text, [FromQuery] string userName)
        {
            var vm = await Mediator.Send(new CreateReviewCommand { BookName = bookName, Stars = stars, Text = text, UserName = userName });
            return vm;
        }

        [HttpDelete]
        public async Task<Unit> DeleteReview([FromQuery] int reviewId)
        {
            var vm = await Mediator.Send(new DeleteReviewCommand { ReviewId = reviewId });
            return vm;
        }

        [HttpPatch]
        public async Task<Unit> UpdateReview([FromQuery] int reviewId, [FromQuery] int stars, [FromQuery] string text)
        {
            var vm = await Mediator.Send(new UpdateReviewCommand { ReviewId = reviewId, Stars = stars, Text = text });
            return vm;
        }
    }
}
