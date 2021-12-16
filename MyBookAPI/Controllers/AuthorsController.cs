using Microsoft.AspNetCore.Mvc;
using MyBookAPI.Application.Authors.Commands.DeleteAuthor;
using MyBookAPI.Application.Authors.Commands.UpdateAuthor;
using MyBookAPI.Application.Common.Authors.Commands.CreateAuthor;
using MyBookAPI.Application.Common.Authors.Queries.GetAuthorDetail;
using System.Threading.Tasks;

namespace MyBookAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<AuthorDetailVm>> GetDetails([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var vm = await Mediator.Send(new GetAuthorDetailQuery() { FirstName = firstName, LastName = lastName });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(DeleteAuthorCommand command)
        { 
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
