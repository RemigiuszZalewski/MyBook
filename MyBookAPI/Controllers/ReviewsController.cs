using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Reviews.Commands.CreateReview;
using MyBookAPI.Application.Reviews.Commands.DeleteReview;
using MyBookAPI.Application.Reviews.Commands.UpdateReview;
using MyBookAPI.Application.Reviews.Queries.GetReviews;
using System;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        /// <summary>
        /// Method that returns a list of reviews about specified book.
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        [HttpGet("{bookName}")]
        public async Task<ActionResult<ReviewsVm>> GetReviews(string bookName)
        {
            var vm = await Mediator.Send(new GetReviewsQuery { BookName = bookName });
            return Ok(vm);
        }

        /// <summary>
        /// Method that creates a review about specified book.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateReview(CreateReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + result), command);
        }

        /// <summary>
        /// Method that deletes specified review.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteReview(DeleteReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Method that updates a review - stars, text or both if needed.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateReview(UpdateReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
