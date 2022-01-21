using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Authors.Commands.DeleteAuthor;
using MyBookAPI.Application.Authors.Commands.UpdateAuthor;
using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;
using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;
using System;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        /// <summary>
        /// Method that gets all details about an author passed into request.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<AuthorDetailVm>> GetDetails([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var vm = await Mediator.Send(new GetAuthorDetailQuery() { FirstName = firstName, LastName = lastName });
            return Ok(vm);
        }

        /// <summary>
        /// Method that creates a new author.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
        {
            var result = await Mediator.Send(command);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + result), command);
        }

        /// <summary>
        /// Method that updates selected information about an author.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Method that deletes an author.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(DeleteAuthorCommand command)
        { 
            var result = await Mediator.Send(command);
            return NoContent();
        }
    }
}
