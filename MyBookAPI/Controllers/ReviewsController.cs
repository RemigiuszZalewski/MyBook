using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Reviews.Commands.CreateReview;
using MyBookAPI.Application.Reviews.Commands.DeleteReview;
using MyBookAPI.Application.Reviews.Commands.UpdateReview;
using MyBookAPI.Application.Reviews.Queries.GetReviews;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        [HttpGet("{bookName}")]
        public async Task<ActionResult<ReviewsVm>> GetReviews(string bookName)
        {
            var vm = await Mediator.Send(new GetReviewsQuery { BookName = bookName });
            return vm;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateReview(CreateReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview(DeleteReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateReview(UpdateReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
